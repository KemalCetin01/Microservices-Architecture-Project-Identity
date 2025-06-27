using MS.Services.Core.Base.IoC;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2CUser;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface IAuthB2CService : IScopedService
{
    Task<AuthenticationDTO> B2CLoginAsync(B2CLoginDTO loginDTO, CancellationToken cancellationToken);
    Task<AuthenticationDTO> B2CRefreshTokenLoginAsync(string refreshToken, CancellationToken cancellationToken);
    Task<SignUpDTO> B2CSignUpAsync(B2CSignUpCommandDTO model, CancellationToken cancellationToken);
    Task<bool> B2CVerifyOtpAsync(VerifyOtpDTO model, CancellationToken cancellationToken);
    Task<bool> B2CMoreQuestionsAsync(B2CMoreQuestionsCommandDTO model, CancellationToken cancellationToken);
    Task<ResendOtpDTO> B2CResendOtpAsync(CancellationToken cancellationToken);
    Task<VerifyOtpDTO> ResetPasswordAsync(ResetPasswordCommandDTO model, UserTypeEnum platform, CancellationToken cancellationToken);
    Task<ResetVerifyOtpDTO> ResetVerifyOtpAsync(VerifyOtpDTO model, UserTypeEnum platform, CancellationToken cancellationToken);
    Task<bool> ChangePasswordAsync(ChangePasswordCommandDTO model,string realm, CancellationToken cancellationToken);
   

}
