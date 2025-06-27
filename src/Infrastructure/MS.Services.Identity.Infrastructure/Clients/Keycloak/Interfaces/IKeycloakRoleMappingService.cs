using MS.Services.Core.Base.IoC;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;
public interface IKeycloakRoleMappingService : IScopedService
{

    Task<KeycloackDataResponse<bool>> CreateRealmRoleMappingForUserAsync(string realm, string userId, List<RoleRepresentation> role, CancellationToken cancellationToken  );
    Task<KeycloackDataResponse<bool>> UpdateRealmRoleMappingForUserAsync(string realm, string userId, List<RoleRepresentation> deletedRole, List<RoleRepresentation> newRole, CancellationToken cancellationToken);
    Task<KeycloackDataResponse<bool>> DeleteRealmRoleMappingForUserAsync(string realm, string userId, List<RoleRepresentation> role, CancellationToken cancellationToken  );
    Task<KeycloackDataResponse<bool>> CreateRealmRoleMappingForGroupAsync(string realm, string groupId, List<RoleRepresentation> roleList, CancellationToken cancellationToken  );
    Task<List<RoleRepresentation>?> GetGroupClientRoleMappingsAsync(string realm, string clientRefId, string groupId, CancellationToken cancellationToken);
    Task<List<RoleRepresentation>?> GetClientRoleMappingsAsync(string realm, string clientRefId, CancellationToken cancellationToken);
    Task<bool> AddGroupClientRolesAsync(string realm, string clientRefId, string groupId, List<RoleRepresentation> roles, CancellationToken cancellationToken);
    Task<bool> DeleteGroupClientRolesAsync(string realm, string clientRefId, string groupId, List<RoleRepresentation> roles, CancellationToken cancellationToken);

}
