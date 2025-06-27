using MS.Services.Core.Base.IoC;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface IAuthTokenService : IScopedService
{
    IdentityUserInfoResponseDto GetUserDetailsFromJwtToken(string token, CancellationToken cancellationToken);

}
