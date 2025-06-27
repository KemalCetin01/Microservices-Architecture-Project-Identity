using MS.Services.Core.Base.IoC;
using MS.Services.Identity.Application.Handlers.B2bRoles.Commands;
using MS.Services.Identity.Application.Handlers.B2bRoles.DTOs;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.Commands;
using MS.Services.Identity.Application.Handlers.Keycloak.Commands;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;
public interface IKeycloakRoleService : IScopedService
{
    Task<RoleRepresentation> GetRoleDetailByName(string realm, string roleName, CancellationToken cancellationToken);
    Task<List<RoleRepresentation>> GetRealmRoleCompositesByNameAsync(string realm, string roleName, CancellationToken cancellationToken);
    Task<List<RoleRepresentation>> GetRealmRoleCompositesByIdAsync(string realm, string roleId, CancellationToken cancellationToken);
    Task<bool> CreateRoleAsync(string realm, RoleRepresentation roleRepresentation, CancellationToken cancellationToken);
    Task<bool> UpdateRoleAsync(string realm, RoleRepresentation roleRepresentation, CancellationToken cancellationToken);
    Task<bool> DeleteRoleAsync(string realm, string role, CancellationToken cancellationToken);
    Task<List<RoleRepresentation>> GetRealmRolesAsync(string realm, CancellationToken cancellationToken);
    Task<KeycloackDataResponse<bool>> AddAssociatedRoleAsync(string realm, string roleId, IEnumerable<RoleRepresentation> roleRepresentations, CancellationToken cancellationToken);
    Task<KeycloackDataResponse<bool>> DeleteAssociatedRoleAsync(string realm, string roleName, IEnumerable<RoleRepresentation> roleRepresentations, CancellationToken cancellationToken);
    Task<bool> SetRolePermissions(string realm, string roleName, List<string>? permissionIds, CancellationToken cancellationToken);
    Task<KeycloackDataResponse<bool>> SetGroupRolePermissions(SetGroupPermissionsCommandDTO request, string realm, CancellationToken cancellationToken);
    Task<List<ClientPermissionModel>> GetGroupRolePermissions(string groupId, string realm, string clientName, CancellationToken cancellationToken);
    Task<List<ClientPermissionModel>> GetChildGroupRolePermissions(string parentGroupId, string childGroupId, string realm, string clientName, CancellationToken cancellationToken);
    Task MigrationClientRoles(KeycloakMigrateClientRolesCommand keycloakMigrateClientRolesCommand, CancellationToken cancellationToken);
    Task AssignAllClientPermsToRealmRole(string realm, string clientId, string realmRoleId, CancellationToken cancellationToken);
}