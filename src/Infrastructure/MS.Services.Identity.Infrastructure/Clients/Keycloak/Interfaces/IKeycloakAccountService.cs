using MS.Services.Core.Base.IoC;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;

public interface IKeycloakAccountService : IScopedService
{
    Task<TokenModel> LoginAsync(KeycloakLoginModel keycloakLoginModel, CancellationToken cancellationToken);
    Task<TokenModel> RefreshTokenLoginAsync(RefreshTokenLoginModel refreshTokenLoginModel, CancellationToken cancellationToken);
}
