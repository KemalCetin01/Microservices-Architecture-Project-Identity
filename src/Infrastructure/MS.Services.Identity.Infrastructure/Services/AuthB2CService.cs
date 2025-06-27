using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Dtos;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services.File;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.DTOs.Identity.Request;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2CUser;
using MS.Services.Identity.Application.Helpers.Options;
using MS.Services.Identity.Application.Helpers.Utility;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Domain.Exceptions;
using Serilog;
using System.Text.RegularExpressions;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Infrastructure.Services;

public class AuthB2CService : IAuthB2CService
{
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IUserB2BRepository _userB2BRepository;
    private readonly IUserB2CRepository _userB2CRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly OtpOptions _otpOptions;
    private readonly IUserB2BService _userB2BService;
    private readonly IBusinessUserRepository _businessUserRepository;
    private readonly IBusinessRepository _businessRepository;
    private readonly IUserOTPRepository _userOTPRepository;
    private readonly IUserResetPasswordRepository _userResetPasswordRepository;
    private readonly IIdentityIdSequenceRepository _identityIdSequenceRepository;
    private readonly IUserConfirmRegisterTypeRepository _userConfirmRegisterTypeRepository;
    private readonly IIdentityB2CService _identityB2CService;
    private readonly HeaderContext _headerContext;
    private readonly IFileService _fileService;
    private static readonly Serilog.ILogger Logger = Log.ForContext<UserB2CService>();

    public AuthB2CService(IIdentityUnitOfWork identityUnitOfWork,
                                 IActivityAreaRepository activityAreaRepository,
                                 IMapper mapper,
                                 IUserB2BRepository userB2BRepository,
                                 IUserRepository userRepository,
                                 IUserB2CRepository userB2CRepository,
                                 IOptions<OtpOptions> otpOptions,
                                 IUserB2BService userB2BService,
                                 IBusinessUserRepository businessUserRepository,
                                 IBusinessRepository businessRepository,
                                 IUserOTPRepository userOTPRepository,
                                 IIdentityIdSequenceRepository identityIdSequenceRepository,
                                 IUserConfirmRegisterTypeRepository userConfirmRegisterTypeRepository,
                                 HeaderContext headerContext,
                                 IFileService fileService,
                                 IUserResetPasswordRepository userResetPasswordRepository,
                                 IIdentityB2CService identityB2CService)
    {
        _identityUnitOfWork = identityUnitOfWork;
        _mapper = mapper;
        _otpOptions = otpOptions.Value;
        _userB2BRepository = userB2BRepository;
        _userB2CRepository = userB2CRepository;
        _userRepository = userRepository;
        _userB2BService = userB2BService;
        _businessUserRepository = businessUserRepository;
        _userOTPRepository = userOTPRepository;
        _identityIdSequenceRepository = identityIdSequenceRepository;
        _businessRepository = businessRepository;
        _userConfirmRegisterTypeRepository = userConfirmRegisterTypeRepository;
        _headerContext = headerContext;
        _fileService = fileService;
        _userResetPasswordRepository = userResetPasswordRepository;
        _identityB2CService = identityB2CService;
    }

    public async Task<AuthenticationDTO> B2CLoginAsync(B2CLoginDTO loginDTO, CancellationToken cancellationToken)
    {
        string Email = await ValidateB2CLoginAsync(loginDTO, cancellationToken);

        var authentication = await _identityB2CService.LoginAsync(loginDTO, cancellationToken);

        return _mapper.Map<AuthenticationDTO>(authentication);
    }

    public async Task<AuthenticationDTO> B2CRefreshTokenLoginAsync(string refreshToken, CancellationToken cancellationToken)
    {
        AuthenticationDTO authentication = await _identityB2CService.RefreshTokenLoginAsync(refreshToken, cancellationToken);
        return _mapper.Map<AuthenticationDTO>(authentication);
    }



    public async Task<SignUpDTO> B2CSignUpAsync(B2CSignUpCommandDTO model, CancellationToken cancellationToken)
    {
        await ValidateB2CSignUpAsync(model, cancellationToken);

        User userEntity = B2CSignUpCommandToUser(model);


        #region Identity insert
        CreateIdentityUserRequestDto createIdentityUserRequestDto = new CreateIdentityUserRequestDto
        {
            UserId = userEntity.Id,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Password = model.Password
        };

        var identityUserResponse = await _identityB2CService.CreateUserAsync(createIdentityUserRequestDto, cancellationToken);


        if (identityUserResponse.IsSuccess == false)
            throw new ValidationException(UserStatusCodes.KeycloakError.Message, UserStatusCodes.KeycloakError.StatusCode);
        #endregion
        UserB2C userB2CEntity = B2CSignUpCommandToUserB2C(model, userEntity);

        userB2CEntity.User.IdentityRefId = identityUserResponse.IdentityRefId;

        await _userB2CRepository.AddAsync(userB2CEntity, cancellationToken);

        var userOTP = await AddUserOtpAsync(userEntity.Id, model.Phone, model.Email, OtpTypeEnum.SignUp, UserTypeEnum.B2C, model.VerificationType, cancellationToken);
        try
        {
            await _identityUnitOfWork.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            var isDeleted = await _identityB2CService.DeleteUserAsync(identityUserResponse.IdentityRefId, cancellationToken);
            if (!isDeleted)
            {
                Logger.ForContext("KeycloakUserId", identityUserResponse.IdentityRefId).Error("There is an error when trying to create b2c user. Keycloak user can not be rollbacked...");
            }
            throw;
        }

        var authentication = await B2CLoginAsync(new B2CLoginDTO() { EmailOrPhone = model.Email, Password = model.Password }, cancellationToken);

        return new SignUpDTO() { TransactionId = userOTP.Id, OTPCode = userOTP.OtpCode, authenticationDTO = authentication };
    }

    public async Task<bool> B2CVerifyOtpAsync(VerifyOtpDTO model, CancellationToken cancellationToken)
    {
        UserOTP userOtpDetail = await ValidateB2CVerifyOtpAsync(model, cancellationToken);
        var userB2CEntity = await _userB2CRepository.GetById(userOtpDetail.UserId, cancellationToken);

        if (userB2CEntity == null)
            throw new ApiException(B2CUserConstants.RecordNotFound);

        userOtpDetail.IsVerified = true;
        userOtpDetail.VerificationDate = DateTime.UtcNow;
        _userOTPRepository.Update(userOtpDetail);

        userB2CEntity.SiteStatus = SiteStatusEnum.Open;
        _userB2CRepository.Update(userB2CEntity);

        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return true;
    }

    public async Task<bool> B2CMoreQuestionsAsync(B2CMoreQuestionsCommandDTO model, CancellationToken cancellationToken)
    {
        var b2cUserDetail = await _userB2CRepository.GetById(_headerContext.ODRefId, cancellationToken);
        if (b2cUserDetail == null)
            throw new ValidationException(UserStatusCodes.UserNotFound.Message, UserStatusCodes.UserNotFound.StatusCode);

        b2cUserDetail.SectorId = model.SectorId;
        b2cUserDetail.OccupationId = model.OccupationId;
        b2cUserDetail.ActivityAreaId = model.ActivityAreaId;

        _userB2CRepository.Update(b2cUserDetail);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
        return true;
    }

    public async Task<ResendOtpDTO> B2CResendOtpAsync(CancellationToken cancellationToken)
    {
        UserB2C userB2CEntity = await ValidateB2CResendOtpAsync(cancellationToken);

        var userOTP = await AddUserOtpAsync(userB2CEntity.UserId, userB2CEntity.Phone, userB2CEntity.User.Email, OtpTypeEnum.SignUp, UserTypeEnum.B2C, VerificationTypeEnum.Email, cancellationToken);
       
        userB2CEntity.SiteStatus = SiteStatusEnum.Open;
        _userB2CRepository.Update(userB2CEntity);

        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return new ResendOtpDTO() { TransactionId = userOTP.Id, OTPCode = userOTP.OtpCode };
    }


    public async Task<VerifyOtpDTO> ResetPasswordAsync(ResetPasswordCommandDTO resetPasswordCommandDTO, UserTypeEnum platform, CancellationToken cancellationToken)
    {
        var userDetail = await _userRepository.GetByEmailAsync(resetPasswordCommandDTO.Email, platform, cancellationToken);

        if (userDetail == null)
            throw new ValidationException(UserStatusCodes.UserNotFound.Message, UserStatusCodes.UserNotFound.StatusCode);

        UserOTP userOtp = SetUserOtp(resetPasswordCommandDTO.Email, platform, userDetail.Id);

        _userOTPRepository.Add(userOtp);

        UserResetPassword userResetPassword = SetUserResetPassword(userDetail.Id, userOtp.Id);

        _userResetPasswordRepository.Add(userResetPassword);

        await _identityUnitOfWork.CommitAsync(cancellationToken);
        return new VerifyOtpDTO() { TransactionId = userOtp.Id, OtpCode = userOtp.OtpCode };
    }

    public async Task<ResetVerifyOtpDTO> ResetVerifyOtpAsync(VerifyOtpDTO model, UserTypeEnum platform, CancellationToken cancellationToken)
    {
        var userOtpDetail = await _userOTPRepository.GetVerifyByIdAsync(model.TransactionId, platform, OtpTypeEnum.ResetPassword, cancellationToken);

        if (userOtpDetail == null)
            throw new ApiException("Reset Expire Time Süresi Aşıldı. Uygun kayıt bulunamadı.");

        if (userOtpDetail.OtpCode != model.OtpCode)
            throw new ApiException("OtpCode Hatalı!");

        userOtpDetail.IsVerified = true;
        userOtpDetail.VerificationDate = DateTime.UtcNow;
        _userOTPRepository.Update(userOtpDetail);

        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return new ResetVerifyOtpDTO() { TransactionId = userOtpDetail.UserResetPassword.Id };
    }

    public async Task<bool> ChangePasswordAsync(ChangePasswordCommandDTO model, string realm, CancellationToken cancellationToken)
    {
        UserResetPassword resetPasswordDetail = await _userResetPasswordRepository.GetVerifyByIdAsync(model.TransactionId, cancellationToken);

        User userDetail = await ValidateChangePasswordAsync(resetPasswordDetail, cancellationToken);

        resetPasswordDetail.IsUsed = true;
        resetPasswordDetail.ResetPasswordDate = DateTime.UtcNow;
        _userResetPasswordRepository.Update(resetPasswordDetail);

        UpdateIdentityUserPasswordRequestDto updateIdentityUserPasswordRequestDto = new UpdateIdentityUserPasswordRequestDto
        {
            IdentityRefId = userDetail.IdentityRefId,
            Password = model.Password
        };

        var result = await _identityB2CService.UpdateUserPasswordAsync(updateIdentityUserPasswordRequestDto, cancellationToken);
        if (result)
        {
            await _identityUnitOfWork.CommitAsync(cancellationToken);
            return true;
        }

        return false;
    }

    private async Task<User> ValidateChangePasswordAsync(UserResetPassword resetPasswordDetail, CancellationToken cancellationToken)
    {
        if (resetPasswordDetail == null)
            throw new ApiException("Expire Time Süresi Aşıldı. Uygun kayıt bulunamadı.");

        var userDetail = await _userRepository.GetById(resetPasswordDetail.UserId, cancellationToken);

        if (userDetail == null)
            throw new ValidationException(UserStatusCodes.UserNotFound.Message, UserStatusCodes.UserNotFound.StatusCode);

        return userDetail;
    }

    private async Task<string> ValidateB2CLoginAsync(B2CLoginDTO loginDTO, CancellationToken cancellationToken)
    {
        string Email = String.Empty;

        if (RegexEmail(loginDTO.EmailOrPhone))
        {
            var b2cDetail = await _userB2CRepository.GetByEmailAsync(loginDTO.EmailOrPhone, cancellationToken);

            if (b2cDetail == null)
                throw new ValidationException(UserStatusCodes.UserNotFound.Message, UserStatusCodes.UserNotFound.StatusCode);
            else if (b2cDetail.SiteStatus == SiteStatusEnum.Closed)
                throw new ValidationException(UserStatusCodes.UserInActive.Message, UserStatusCodes.UserInActive.StatusCode);
            else
                Email = b2cDetail.User.Email;
        }
        else if (RegexPhone(loginDTO.EmailOrPhone))
        {
            var b2cDetail = await _userB2CRepository.GetByPhoneAsync(loginDTO.EmailOrPhone, cancellationToken);
            if (b2cDetail == null)
                throw new ValidationException(UserStatusCodes.UserNotFound.Message, UserStatusCodes.UserNotFound.StatusCode);
            else if (b2cDetail.SiteStatus == SiteStatusEnum.Closed)
                throw new ValidationException(UserStatusCodes.UserInActive.Message, UserStatusCodes.UserInActive.StatusCode);
            else Email = b2cDetail.User.Email;
        }

        if (string.IsNullOrEmpty(Email))
            throw new ValidationException(UserStatusCodes.InvalidEmailOrPhone.Message, UserStatusCodes.InvalidEmailOrPhone.StatusCode);

        return Email;
    }

    private UserResetPassword SetUserResetPassword(Guid userId, Guid transactionId)
    {
        return new UserResetPassword()
        {
            CreatedDate = DateTime.UtcNow,
            IsUsed = false,
            ExpireDate = DateTimeHelper.AddSecondsUtc(_otpOptions.reset_expire_time),
            UserId = userId,
            UserOtpId = transactionId
        };
    }

    private UserOTP SetUserOtp(string email, UserTypeEnum platform, Guid userId)
    {
        return new UserOTP
        {
            Email = email,
            ExpireDate = DateTimeHelper.AddSecondsUtc(_otpOptions.expire_time),
            VerificationType = VerificationTypeEnum.Email,
            OtpCode = GenerateOTP(),
            OtpType = OtpTypeEnum.ResetPassword,
            IsVerified = false,
            Platform = platform,
            UserId = userId,
            CreatedDate = DateTime.UtcNow
        };
    }

    private async Task<UserOTP> ValidateB2CVerifyOtpAsync(VerifyOtpDTO model, CancellationToken cancellationToken)
    {
        var userOtpDetail = await _userOTPRepository.GetVerifyByIdAsync(model.TransactionId, UserTypeEnum.B2C, OtpTypeEnum.SignUp, cancellationToken);

        if (userOtpDetail == null)
            throw new ApiException("Expire Time Exceeded. No suitable record found.");

        if (userOtpDetail.OtpCode != model.OtpCode)
            throw new ApiException("The OtpCode does not match.");
        return userOtpDetail;
    }

    private string GenerateOTP()
    {
        string numbers = NumberSequence;
        string otp = string.Empty;
        for (int i = 0; i < OtpLenght; i++)
        {
            string character = string.Empty;
            do
            {
                int index = new Random().Next(0, numbers.Length);
                character = numbers.ToCharArray()[index].ToString();
            } while (otp.IndexOf(character) != -1);
            otp += character;
        }
        return otp;
    }

    private bool RegexPhone(string EmailOrPhone)
    {
        Regex validatePhoneNumberRegex = new Regex("^\\+?[1-9][0-9]{3,15}$");
        return validatePhoneNumberRegex.IsMatch(EmailOrPhone);
    }

    private bool RegexEmail(string EmailOrPhone)
    {
        Regex validateEmailRegex = new Regex("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");
        return validateEmailRegex.IsMatch(EmailOrPhone);
    }

    private async Task<UserOTP> AddUserOtpAsync(Guid userId, string? phone, string? email, OtpTypeEnum otpType, UserTypeEnum platform, VerificationTypeEnum verificationMethod, CancellationToken cancellationToken)
    {
        UserOTP userOTP = new UserOTP
        {
            UserId = userId,
            Phone = phone,
            Email = email,
            ExpireDate = DateTimeHelper.AddSecondsUtc(_otpOptions.expire_time),
            OtpCode = GenerateOTP(),
            IsVerified = false,
            OtpType = otpType,
            Platform = platform,
            VerificationType = verificationMethod,
            CreatedDate = DateTime.UtcNow
        };

        await _userOTPRepository.AddAsync(userOTP, cancellationToken);
        return userOTP;
    }

    private UserB2C B2CSignUpCommandToUserB2C(B2CSignUpCommandDTO model, User userEntity)
    {
        return new UserB2C
        {
            User = userEntity,
            UserId = userEntity.Id,
            CountryId = model.CountryId,
            CityId = model.CityId,
            Phone = model.Phone,
            PhoneCountryCode = model.PhoneCountryCode,
            SiteStatus = SiteStatusEnum.Closed,
            UserStatus = UserStatusEnum.Active
        };
    }

    private User B2CSignUpCommandToUser(B2CSignUpCommandDTO model)
    {
        return new User
        {
            Email = model.Email,
            Suffix = model.Suffix,
            FirstName = model.FirstName,
            LastName = model.LastName,
            UserTypeId = (int)UserTypeEnum.B2C,
            UserConfirmRegisterTypes = model.ConfirmRegisters.Select(x => new UserConfirmRegisterType { ConfirmRegisterTypeId = (int)x }).ToList()
        };
    }

    private async Task ValidateB2CSignUpAsync(B2CSignUpCommandDTO model, CancellationToken cancellationToken)
    {
        if (model.VerificationType == VerificationTypeEnum.Phone && model.CountryCode != TurkeyCountryCode)
            throw new ValidationException(UserStatusCodes.VerificationTypeControl.Message, UserStatusCodes.VerificationTypeControl.StatusCode);

        if (!RegexEmail(model.Email))
            throw new ValidationException(UserStatusCodes.InvalidEmail.Message, UserStatusCodes.InvalidEmail.StatusCode);

        var hasExistsEmail = await _userRepository.AnotherUserHasEmailAsync(null, UserTypeEnum.B2C, model.Email, cancellationToken);
        if (hasExistsEmail)
            throw new ValidationException(UserStatusCodes.EmailConflict.Message, UserStatusCodes.EmailConflict.StatusCode);

        var hasExistsPhone = await _userB2CRepository.AnotherUserHasPhoneAsync(null, model.PhoneCountryCode, model.Phone, cancellationToken);
        if (hasExistsPhone)
            throw new ValidationException(UserStatusCodes.PhoneConflict.Message, UserStatusCodes.PhoneConflict.StatusCode);
    }

    private async Task<UserB2C> ValidateB2CResendOtpAsync(CancellationToken cancellationToken)
    {
        var userB2CEntity = await _userB2CRepository.GetById(_headerContext.ODRefId.Value, cancellationToken);

        if (userB2CEntity == null)
            throw new ApiException(B2CUserConstants.RecordNotFound);
        var isVerified = await _userOTPRepository.IsVerifiedAsync(_headerContext.ODRefId.Value, UserTypeEnum.B2C, OtpTypeEnum.SignUp, cancellationToken);

        if (isVerified)
            throw new ApiException(IsVerifiedUser);
        return userB2CEntity;
    }
}
