using MS.Services.Core.Base.IoC;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;
public interface IKeycloakClientService : IScopedService
{
    Task<List<ClientPermissionModel>> GetPermissionsAsync(string realms, string roleName, CancellationToken cancellationToken = default);
}
