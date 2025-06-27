using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2CUser;

namespace MS.Services.Identity.Application.Core.Infrastructure.External.Identity;

public interface IIdentityB2CService : IIdentityBaseService
{
    Task<AuthenticationDTO> LoginAsync(B2CLoginDTO request, CancellationToken cancellationToken);

    Task<AuthenticationDTO> RefreshTokenLoginAsync(string refreshToken, CancellationToken cancellationToken);
}