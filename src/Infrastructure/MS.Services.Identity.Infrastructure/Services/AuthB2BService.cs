using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Dtos;
using MS.Services.Core.Base.Enums;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.DTOs.Identity.Request;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;
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
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly KeycloakOptions _keycloakOptions;
    private readonly OtpOptions _otpOptions;
    private readonly IUserOTPRepository _userOTPRepository;
    private readonly IUserResetPasswordRepository _userResetPasswordRepository;
    private readonly IIdentityIdSequenceRepository _identityIdSequenceRepository;
    private readonly IUserConfirmRegisterTypeRepository _userConfirmRegisterTypeRepository;
    private readonly HeaderContext _headerContext;

    public AuthB2BService(IIdentityUnitOfWork identityUnitOfWork,
                                 IMapper mapper,
                                 IUserB2BRepository userB2BRepository,
                                 IUserRepository userRepository,
                                 IOptions<KeycloakOptions> options,
                                 IOptions<OtpOptions> otpOptions,
                                 IUserOTPRepository userOTPRepository,
                                 IIdentityIdSequenceRepository identityIdSequenceRepository,
                                 IUserConfirmRegisterTypeRepository userConfirmRegisterTypeRepository,
                                 HeaderContext headerContext,
                                 IUserResetPasswordRepository userResetPasswordRepository)
    {
        _identityUnitOfWork = identityUnitOfWork;
        _mapper = mapper;
        _keycloakOptions = options.Value;
        _otpOptions = otpOptions.Value;
        _userB2BRepository = userB2BRepository;
        _userRepository = userRepository;
        _userOTPRepository = userOTPRepository;
        _identityIdSequenceRepository = identityIdSequenceRepository;
        _userConfirmRegisterTypeRepository = userConfirmRegisterTypeRepository;
        _headerContext = headerContext;
        _userResetPasswordRepository = userResetPasswordRepository;
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

        // var userB2BEntity = await _userB2BRepository.GetById(userOtpDetail.UserId, cancellationToken);
        var userB2BEntity= 0;
        if (userB2BEntity == null)
            throw new ApiException(B2BUserConstants.RecordNotFound);

        if (userOtpDetail == null)
            throw new ApiException("Expire Time Süresi Aşıldı. Uygun kayıt bulunamadı.");

        if (userOtpDetail.OtpCode != model.OtpCode)
            throw new ApiException("OtpCode Hatalı!");

        userOtpDetail.IsVerified = true;
        userOtpDetail.VerificationDate = DateTime.UtcNow;
        _userOTPRepository.Update(userOtpDetail);

        //userB2BEntity.SiteStatus = SiteStatusEnum.Open;
        //_userB2BRepository.Update(userB2BEntity);

        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return true;
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


    private async Task<User> ValidateChangePasswordAsync(UserResetPassword resetPasswordDetail, CancellationToken cancellationToken)
    {
        if (resetPasswordDetail == null)
            throw new ApiException("Expire Time Süresi Aşıldı. Uygun kayıt bulunamadı.");

        var userDetail = await _userRepository.GetById(resetPasswordDetail.UserId, cancellationToken);

        if (userDetail == null)
            throw new ValidationException(UserStatusCodes.UserNotFound.Message, UserStatusCodes.UserNotFound.StatusCode);

        return userDetail;
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

    public Task<AuthenticationDTO> B2BLoginAsync(B2BLoginDTO loginDTO, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<AuthenticationDTO> B2BRefreshTokenLoginAsync(string refreshToken, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<SignUpDTO> B2BSignUpAsync(B2BSignupCommandDTO model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ResendOtpDTO> B2BResendOtpAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangePasswordAsync(ChangePasswordCommandDTO model, string realm, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
