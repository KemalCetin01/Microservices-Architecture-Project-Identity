using MS.Services.Core.Base.IoC;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2CUser;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface IAuthB2BService : IScopedService
{
    Task<AuthenticationDTO> B2BLoginAsync(B2BLoginDTO loginDTO, CancellationToken cancellationToken);
    Task<AuthenticationDTO> B2BRefreshTokenLoginAsync(string refreshToken, CancellationToken cancellationToken);
    Task<SignUpDTO> B2BSignUpAsync(B2BSignupCommandDTO model, CancellationToken cancellationToken);
    Task<bool> B2BVerifyOtpAsync(VerifyOtpDTO model, CancellationToken cancellationToken);
    Task<ResendOtpDTO> B2BResendOtpAsync(CancellationToken cancellationToken);
    Task<VerifyOtpDTO> ResetPasswordAsync(ResetPasswordCommandDTO model, UserTypeEnum platform, CancellationToken cancellationToken);
    Task<ResetVerifyOtpDTO> ResetVerifyOtpAsync(VerifyOtpDTO model, UserTypeEnum platform, CancellationToken cancellationToken);
    Task<bool> ChangePasswordAsync(ChangePasswordCommandDTO model,string realm, CancellationToken cancellationToken);
   

}
