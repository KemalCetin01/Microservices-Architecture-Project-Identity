using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Dtos;
using MS.Services.Core.Base.Enums;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services.File;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.DTOs.Identity.Request;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;
using MS.Services.Identity.Application.Handlers.Files.DTOs.Request;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Application.Helpers.Options;
using MS.Services.Identity.Application.Helpers.Utility;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Domain.Exceptions;
using Serilog;
using System.Text.RegularExpressions;
using static MS.Services.Identity.Application.Constants.Constants;
using BusinessConstants = MS.Services.Identity.Domain.EntityConstants.BusinessConstants;

namespace MS.Services.Identity.Infrastructure.Services;

public class AuthB2BService : IAuthB2BService
{
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IUserB2BRepository _userB2BRepository;
    private readonly IUserB2CRepository _userB2CRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly KeycloakOptions _keycloakOptions;
    private readonly OtpOptions _otpOptions;
    private readonly IUserB2BService _userB2BService;
    private readonly IBusinessUserRepository _businessUserRepository;
    private readonly IBusinessRepository _businessRepository;
    private readonly IUserOTPRepository _userOTPRepository;
    private readonly IUserResetPasswordRepository _userResetPasswordRepository;
    private readonly IIdentityIdSequenceRepository _identityIdSequenceRepository;
    private readonly IUserConfirmRegisterTypeRepository _userConfirmRegisterTypeRepository;
    private readonly IIdentityB2BService _identityB2BService;
    private readonly HeaderContext _headerContext;
    private readonly IFileService _fileService;
    private static readonly Serilog.ILogger Logger = Log.ForContext<UserB2CService>();

    public AuthB2BService(IIdentityUnitOfWork identityUnitOfWork,
                                 IActivityAreaRepository activityAreaRepository,
                                 IMapper mapper,
                                 IUserB2BRepository userB2BRepository,
                                 IUserRepository userRepository,
                                 IUserB2CRepository userB2CRepository,
                                 IOptions<KeycloakOptions> options,
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
                                 IIdentityB2BService identityB2BService)
    {
        _identityUnitOfWork = identityUnitOfWork;
        _mapper = mapper;
        _keycloakOptions = options.Value;
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
        _identityB2BService = identityB2BService;
    }

    public async Task<AuthenticationDTO> B2BLoginAsync(B2BLoginDTO loginDTO, CancellationToken cancellationToken)
    {
        await ValidateB2BLoginAsync(loginDTO, cancellationToken);

        var result = await _identityB2BService.LoginAsync(loginDTO, cancellationToken);

        return _mapper.Map<AuthenticationDTO>(result);
    }


    public async Task<AuthenticationDTO> B2BRefreshTokenLoginAsync(string refreshToken, CancellationToken cancellationToken)
    {
        AuthenticationDTO authentication = await _identityB2BService.RefreshTokenLoginAsync(refreshToken, cancellationToken);
        return _mapper.Map<AuthenticationDTO>(authentication);
    }

    public async Task<SignUpDTO> B2BSignUpAsync(B2BSignupCommandDTO model, CancellationToken cancellationToken)
    {
        var hasExistsEmail = await _userRepository.AnotherUserHasEmailAsync(null, UserTypeEnum.B2B, model.Email, cancellationToken);
        if (hasExistsEmail)
            throw new ValidationException(UserStatusCodes.EmailConflict.Message, UserStatusCodes.EmailConflict.StatusCode);

        User userEntity = B2BSignupCommandToUser(model);

        UserB2B userB2BEntity = B2BSignupCommandToUserB2B(model, userEntity);

        //BillingDetail billingDetailEntity = B2BSignupCommandToBillingDetail(model);

        //Address addressEntity = B2BSignupCommandToAddress(model, billingDetailEntity);

        var businessEntity = await B2BSignupCommandToBusiness(model, null); //TODO add address

        #region Identity user insert
        CreateIdentityUserRequestDto createIdentityUserRequestDto = new CreateIdentityUserRequestDto
        {
            UserId = userEntity.Id,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Password = model.Password
        };


        var identityUser = await _identityB2BService.CreateUserAsync(createIdentityUserRequestDto, cancellationToken);

        if (identityUser.IsSuccess == false)
            throw new ValidationException(UserStatusCodes.KeycloakError.Message, UserStatusCodes.KeycloakError.StatusCode);

        #endregion

        #region Identity business insert
        
        var identityBusinessResponse = await _identityB2BService.CreateBusinessAsync(new CreateIdentityBusinessRequestDto(businessEntity.Code), cancellationToken);

        if (!identityBusinessResponse.IsSuccess)
        { // User oluşturuldu ama business group oluşturulmadıysa; user rollback edilir
            var isDeleted = await _identityB2BService.DeleteUserAsync(identityUser.IdentityRefId, cancellationToken);
            //var keycloakResponse = await _keycloakUserService.DeleteUserAsync(_keycloakOptions.ecommerce_b2b_realm, keycloakUser.IdentityRefId.ToString(), cancellationToken);
            if (!isDeleted)
            {
                Logger.ForContext("KeycloakUserId", identityUser.IdentityRefId).Error("There is an error when trying to create b2b user. Keycloak user can not be rollbacked...");
            }
            throw new ApiException(UserStatusCodes.KeycloakError.Message);
        }

        #endregion

        userB2BEntity.User.IdentityRefId = identityUser.IdentityRefId;


        #region business insert

        businessEntity.IdentityRefId = identityBusinessResponse.IdentityRefId;
        businessEntity.CreatedBy = userB2BEntity.User.Id;

        await _businessRepository.AddAsync(businessEntity, cancellationToken);

        #endregion

        #region user business insert

        await _businessUserRepository.AddAsync(new BusinessUser
        {
            UserId = userB2BEntity.User.Id,
            BusinessId = businessEntity.Id
        });
        #endregion

        #region user and userB2B insert

        await _userB2BRepository.AddAsync(userB2BEntity, cancellationToken);

        #endregion

        #region UserConfirmRegisterType insert

        foreach (var confirmRegister in model.ConfirmRegisters)
        {
            await _userConfirmRegisterTypeRepository.AddAsync(
                new UserConfirmRegisterType()
                {
                    UserId = userB2BEntity.User.Id,
                    ConfirmRegisterTypeId = Convert.ToInt32(confirmRegister)
                });
        }
        #endregion
        var userOTP = await AddUserOtpAsync(userB2BEntity.UserId, userB2BEntity.Phone, userB2BEntity.User.Email, OtpTypeEnum.SignUp, UserTypeEnum.B2B, VerificationTypeEnum.Email, cancellationToken);

        try
        {
            await _identityUnitOfWork.CommitAsync(cancellationToken);
        }
        catch (Exception)
        {
            var isUserDeleted = await _identityB2BService.DeleteUserAsync(identityUser.IdentityRefId, cancellationToken);

            if (!isUserDeleted)
            {
                Logger.ForContext("KeycloakUserId", identityUser.IdentityRefId).Error("There is an error when trying to create b2b user. Keycloak user can not be rollbacked...");
            }
            var isBusinessDeleted = await _identityB2BService.DeleteBusinessAsync(businessEntity.IdentityRefId.Value, cancellationToken);
            if (!isBusinessDeleted)
                throw new ApiException(GroupRoleConstants.ErrorWhenDeleted);
            throw;
        }


        foreach (var file in model.TaxCertificates)
        {
            var filePath = await _fileService.UploadFileToCdnAsync(new FileUploadRequestDto
            {
                File = file,
                AccessControlType = AWSAccessControlType.Private,
                Relation = new FileUploadSubRelationRequestDto
                {
                    Entity = nameof(Domain.Entities.Business),
                    EntityId = businessEntity.Id.ToString(),
                    EntityField = nameof(BusinessConstants.File.TaxPlate)
                }
            }, cancellationToken);
        }


        var authentication = await B2BLoginAsync(new B2BLoginDTO() { Email = model.Email, Password = model.Password }, cancellationToken);

        return new SignUpDTO() { TransactionId = userOTP.Id, OTPCode = userOTP.OtpCode, authenticationDTO = authentication };
    }

    private async Task<Domain.Entities.Business> B2BSignupCommandToBusiness(B2BSignupCommandDTO model, AddressLocation addressEntity)
    {
        var businessEntity = new Domain.Entities.Business
        {
            Name = model.CompanyName,
            BusinessStatusId = (int) BusinessStatusEnum.Active,
            ReviewStatus = ReviewStatusEnum.Pending,
            ActivityAreaId = model.ActivityAreaId,
            SectorId = model.SectorId,
            NumberOfEmployeeId = model.NumberOfEmployeeId,
            //BillingAddress = addressEntity, //TODO convert to billing address
            //BillingAddressId = addressEntity.Id
            //PositionId 
            //OccupationId bu alanlar nereye kaydedilecek?
        };
        var businessKey = await _identityIdSequenceRepository.GetAndUpdateByEntity(nameof(Business));
        if (businessKey != null)
            businessEntity.Code = businessKey.Prefix + "-" + businessKey.Counter;
        else throw new ApiException("Can not generate business code");

        return businessEntity;
    }
/*
    private static Address B2BSignupCommandToAddress(B2BSignupCommandDTO model, BillingDetail billingDetailEntity)
    {
        return new Address
        {
            Name = model.AddressName,
            CompanyName = model.CompanyName,
            ZipCode = model.ZipCode,
            CountryId = model.CountryId,
            CityId = model.CityId,
            AddressLine1 = model.AddressLine1,
            AddressLine2 = model.AddressLine2,
            Region = model.Region,
            PhoneNumber = (model.PhoneCountryCode + model.Phone),
            BillingDetail = billingDetailEntity,
            BillingDetailId = billingDetailEntity.Id
        };
    }
    

    private BillingDetail B2BSignupCommandToBillingDetail(B2BSignupCommandDTO model)
    {
        return new BillingDetail
        {
            TaxNumber = model.TaxNumber,
            Email = model.Email,

        };
    }
*/
    private UserB2B B2BSignupCommandToUserB2B(B2BSignupCommandDTO model, User userEntity)
    {
        return new UserB2B
        {
            User = userEntity,
            UserId = userEntity.Id,
            ActivityAreaId = model.ActivityAreaId,
            CityId = model.CityId,
            CountryId = model.CountryId,
            Phone = model.Phone,
            PhoneCountryCode = model.PhoneCountryCode,
            SectorId = model.SectorId,
            SiteStatus = SiteStatusEnum.Closed,
            UserStatus = UserStatusEnum.Active
        };
    }

    private User B2BSignupCommandToUser(B2BSignupCommandDTO model)
    {
        return new User
        {
            UserTypeId = (int)UserTypeEnum.B2B,
            Email = model.Email,
            FirstName = model.FirstName,
            LastName = model.LastName,
            Suffix = model.Suffix
        };
    }

    public async Task<bool> B2BVerifyOtpAsync(VerifyOtpDTO model, CancellationToken cancellationToken)
    {
        var userOtpDetail = await _userOTPRepository.GetVerifyByIdAsync(model.TransactionId, UserTypeEnum.B2B, OtpTypeEnum.SignUp, cancellationToken);
       
        var userB2BEntity = await _userB2BRepository.GetById(userOtpDetail.UserId, cancellationToken);

        if (userB2BEntity == null)
            throw new ApiException(B2BUserConstants.RecordNotFound);

        if (userOtpDetail == null)
            throw new ApiException("Expire Time Süresi Aşıldı. Uygun kayıt bulunamadı.");

        if (userOtpDetail.OtpCode != model.OtpCode)
            throw new ApiException("OtpCode Hatalı!");

        userOtpDetail.IsVerified = true;
        userOtpDetail.VerificationDate = DateTime.UtcNow;
        _userOTPRepository.Update(userOtpDetail);

        userB2BEntity.SiteStatus = SiteStatusEnum.Open;
        _userB2BRepository.Update(userB2BEntity);

        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return true;
    }

    public async Task<ResendOtpDTO> B2BResendOtpAsync(CancellationToken cancellationToken)
    {
        UserB2B userB2BEntity = await ValidateB2BResendOtpAsync(cancellationToken);
        var userOTP = await AddUserOtpAsync(userB2BEntity.UserId, userB2BEntity.Phone, userB2BEntity.User.Email, OtpTypeEnum.SignUp, UserTypeEnum.B2C, VerificationTypeEnum.Email, cancellationToken);

        userB2BEntity.SiteStatus = SiteStatusEnum.Open;
        _userB2BRepository.Update(userB2BEntity);

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
        var result = await _identityB2BService.UpdateUserPasswordAsync(updateIdentityUserPasswordRequestDto, cancellationToken);

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

    private async Task ValidateB2BLoginAsync(B2BLoginDTO loginDTO, CancellationToken cancellationToken)
    {
        if (RegexEmail(loginDTO.Email))
        {
            UserB2B userB2BEntity = await _userB2BRepository.GetByEmailAsync(loginDTO.Email, cancellationToken);

            if (userB2BEntity == null)
                throw new ValidationException(UserStatusCodes.UserNotFound.Message, UserStatusCodes.UserNotFound.StatusCode);
            else if (userB2BEntity.SiteStatus == SiteStatusEnum.Closed)
                throw new ValidationException(UserStatusCodes.UserInActive.Message, UserStatusCodes.UserInActive.StatusCode);
        }
        else
        { throw new ValidationException(UserStatusCodes.InvalidEmail.Message, UserStatusCodes.InvalidEmail.StatusCode); }
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

    private async Task<UserB2B> ValidateB2BResendOtpAsync(CancellationToken cancellationToken)
    {
        var userB2BEntity = await _userB2BRepository.GetById(_headerContext.ODRefId.Value, cancellationToken);

        if (userB2BEntity == null)
            throw new ApiException(B2CUserConstants.RecordNotFound);

        var isVerified = await _userOTPRepository.IsVerifiedAsync(_headerContext.ODRefId.Value, UserTypeEnum.B2B, OtpTypeEnum.SignUp, cancellationToken);

        if (isVerified)
            throw new ApiException(IsVerifiedUser);
        return userB2BEntity;
    }
}
