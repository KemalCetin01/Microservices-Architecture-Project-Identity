using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Handlers.B2bRoles.DTOs;
using MS.Services.Identity.Application.Handlers.User.Commands.B2BUser;
using MS.Services.Identity.Application.Handlers.User.DTOs;

using MS.Services.Identity.Domain.EntityFilters;
using System.Net;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;


namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;

public interface IKeycloakGroupService : IScopedService
{
    
    Task<ICollection<B2bGroupRoleDTO>> GetAllB2BRolesAsync(string search, Guid identityGroupRefId, string realm, CancellationToken cancellationToken);
    Task<PagedResponse<B2bGroupRoleDTO>> SearchAsync(SearchQueryModel<SearchBusinessRolesQueryFilterModel> searchQuery, Guid identityGroupRefId, string realm, CancellationToken cancellationToken);
    Task<GroupRepresentation?> GetGroupAsync(string realm, string groupId, CancellationToken cancellationToken);
    Task<KeycloakResponse> CreateGroupAsync(string realm, GroupRepresentation groupRepresentation, CancellationToken cancellationToken);
    Task<bool> UpdateGroupAsync(string realm, string groupId, GroupRepresentation groupRepresentation, CancellationToken cancellationToken);
    Task<bool> DeleteGroupAsync(string realm, string groupId, CancellationToken cancellationToken);
    Task<bool> CreateChildGroupAsync(string realm, string parentGroupId, GroupRepresentation groupRepresentation, CancellationToken cancellationToken);
    Task<TempB2BUserRolePermissionDTO> GetB2BGroupsAndPermissionsAsync(string realm, string keycloakUserId, CancellationToken cancellationToken);
    Task<KeycloackDataResponse<bool>> AssignB2BUserGroupsAndPermissionsAsync(SetB2BUserGroupPermissionsCommand request, string realm, CancellationToken cancellationToken);
    Task<KeycloackDataResponse<bool>> UnAssignGroupToUser(string realm, string groupId, string userId, CancellationToken cancellationToken);
    Task<KeycloackDataResponse<bool>> AssignGroupToUser(string realm, string groupId, string userId, CancellationToken cancellationToken);
    Task<List<GroupRepresentation>?> GetUserGroups(string realm, string userId, CancellationToken cancellationToken);

}
