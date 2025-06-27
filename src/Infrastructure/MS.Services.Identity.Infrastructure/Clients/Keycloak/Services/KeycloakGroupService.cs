using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Handlers.B2bRoles.DTOs;
using MS.Services.Identity.Application.Handlers.User.Commands.B2BUser;
using MS.Services.Identity.Application.Handlers.User.DTOs;

using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
using System.Net;
using System.Text;
using System.Text.Json;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Services;

public class KeycloakGroupService : KeycloakBaseService, IKeycloakGroupService
{
    private readonly IMapper _mapper;

    public KeycloakGroupService(IOptions<KeycloakOptions> options, HttpClient client, IMapper mapper, string? baseAddress = null, Dictionary<string, string>? requestHeaders = null) : base(options, client, baseAddress, requestHeaders)
    {
        _mapper = mapper;
        SetRequestHeaders(new Dictionary<string, string> { { "Accept", "application/json" } });
    }
    public SearchListModel<KeycloakGroupModel> SearchModelAsync(SearchQueryModel<SearchBusinessRolesQueryFilterModel> searchQuery, List<KeycloakGroupModel> result)
    {
        /*TODO: Refactor yapılacak. generic yazılacak*/
        if (searchQuery.GlobalSearch != null)
            result = result.Where(x => x.name.ToLower().Contains(searchQuery.GlobalSearch.ToLower())).ToList();

        var rowCount = result.Count();

        if (searchQuery.Filter != null)
            if (searchQuery.Filter.name != null)
                result = result.Where(x => x.name.ToLower().Contains(searchQuery.Filter.name.ToLower())).ToList();

        if (searchQuery.Sort != null)
        {
            if (searchQuery.Sort.Direction.ToUpper() == "DESC")
            {
                result = result.OrderByDescending(x => x.GetType().GetProperty(searchQuery.Sort.Field).GetValue(x, null)).ToList();
            }
            else
            {
                result = result.OrderBy(x => x.GetType().GetProperty(searchQuery.Sort.Field).GetValue(x, null)).ToList();
            }
        }
        if (searchQuery.Pagination != null && searchQuery.Pagination.CurrentPage > 0)
        {
            var skip = (searchQuery.Pagination.CurrentPage - 1) * searchQuery.Pagination.PageSize;
            result = result.Skip(skip).Take(searchQuery.Pagination.PageSize).ToList();
        }

        return new SearchListModel<KeycloakGroupModel>(result, searchQuery.Pagination?.CurrentPage,
            searchQuery.Pagination?.PageSize, rowCount);

    }
    public async Task<PagedResponse<B2bGroupRoleDTO>> SearchAsync(SearchQueryModel<SearchBusinessRolesQueryFilterModel> searchQuery, Guid identityGroupRefId, string realm, CancellationToken cancellationToken)
    {
        await GenerateTokenAsync(cancellationToken);
        var endpoind = $"admin/realms/{realm}/groups/{identityGroupRefId}";

        var result = await GetAsync<KeycloakGroupModel>(endpoind, cancellationToken);

        var searchResult = SearchModelAsync(searchQuery, result.subGroups.ToList());

        return _mapper.Map<PagedResponse<B2bGroupRoleDTO>>(searchResult);
    }
    public async Task<ICollection<B2bGroupRoleDTO>> GetAllB2BRolesAsync(string search, Guid identityGroupRefId, string realm, CancellationToken cancellationToken)
    {
        await GenerateTokenAsync(cancellationToken);
        var endpoind = $"admin/realms/{realm}/groups/{identityGroupRefId}";

        var result = await GetAsync<KeycloakGroupModel>(endpoind, cancellationToken);
        var searchResult = result.subGroups.ToList();
        if (!string.IsNullOrWhiteSpace(search))
        {
            searchResult = result.subGroups.Where(x => x.name.Contains(search)).ToList();

        }
        return _mapper.Map<ICollection<B2bGroupRoleDTO>>(searchResult);

    }

    public async Task<bool> CreateChildGroupAsync(string realm, string parentGroupId, GroupRepresentation keycloakGroupModel, CancellationToken cancellationToken)
    {
        var endpoint = $"admin/realms/{realm}/groups/{parentGroupId}/children";
        var stringContent = new StringContent(JsonSerializer.Serialize(keycloakGroupModel), Encoding.UTF8, "application/json");

        var response = await SendRequestToKeycloakAsync(endpoint, realm, HttpMethod.Post, stringContent, cancellationToken);

        return response.IsSuccessStatusCode;
    }

    public async Task<KeycloakResponse> CreateGroupAsync(string realm, GroupRepresentation groupRepresentation, CancellationToken cancellationToken)
    {
        var endpoind = $"admin/realms/{realm}/groups";
        var stringContent = new StringContent(JsonSerializer.Serialize(groupRepresentation), Encoding.UTF8, "application/json");

        var response = await SendRequestToKeycloakAsync(endpoind, realm, HttpMethod.Post, stringContent, cancellationToken);

        var result = new KeycloakResponse();
        result.IsSuccess = response.IsSuccessStatusCode;

        if (response.Headers.Location != null)
        {
            result.Id = response.Headers.Location.Segments.Last();
        }
        return result;
    }
    public async Task<bool> UpdateGroupAsync(string realm, string groupId, GroupRepresentation groupRepresentation, CancellationToken cancellationToken)
    {
        var endpoint = $"admin/realms/{realm}/groups/{groupId}";
        var stringContent = new StringContent(JsonSerializer.Serialize(groupRepresentation), Encoding.UTF8, "application/json");

        var response = await SendRequestToKeycloakAsync(endpoint, realm, HttpMethod.Put, stringContent, cancellationToken);

        return response.IsSuccessStatusCode;
    }
    public async Task<bool> DeleteGroupAsync(string realm, string groupId, CancellationToken cancellationToken)
    {
        var endpoind = $"admin/realms/{realm}/groups/{groupId}";

        var response = await SendRequestToKeycloakAsync(endpoind, realm, HttpMethod.Delete, null, cancellationToken);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<GroupRepresentation>?> GetUserGroups(string realm, string userId, CancellationToken cancellationToken)
    {
        await GenerateTokenAsync(cancellationToken);
        var endpoind = $"admin/realms/{realm}/users/{userId}/groups";
        return await GetListAsync<GroupRepresentation>(endpoind, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }, cancellationToken);
    }

    public async Task<TempB2BUserRolePermissionDTO> GetB2BGroupsAndPermissionsAsync(string realm, string keycloakUserId, CancellationToken cancellationToken)
    {
        var groups = await GetUserGroups(realm, keycloakUserId, cancellationToken);
        //if (groups.Count() == 0)
        //    return new B2BUserRolePermissionDTO() { };

        var group = groups.FirstOrDefault();// user a birden fazla group atanamaz
        var clientPermissions = new List<ClientPermissionModel>();
        var b2bPermission = new ClientPermissionModel();
        if (groups.Count() != 0)
        {
            clientPermissions = await GetGroupRolePermissions(group.id, realm, KeycloakConstants.ecommerceClient, cancellationToken);
        }
        else
        {
            clientPermissions = await GetGroupRolePermissions("", realm, KeycloakConstants.ecommerceClient, cancellationToken);
        }

        b2bPermission = clientPermissions.FirstOrDefault();
      //  b2bPermission.Permissions.ToList().ForEach(x => x.disabled = !x.selected ? false : true);

        var ecommerceClientDetails = await GetCurrentClients(realm, KeycloakConstants.ecommerceClient, cancellationToken);
        var ecommerClientsId = ecommerceClientDetails.FirstOrDefault().id;


        var assignedUserPerms = await GetUserClientPermissionsAsync<KeycloakRoleModel>(realm, keycloakUserId, ecommerClientsId, cancellationToken);

        foreach (var assignedUserPerm in assignedUserPerms)
        {
            if (b2bPermission.Permissions != null)
            {

                foreach (var perms in b2bPermission.Permissions)
                {
                  /*  if (perms.id == assignedUserPerm.id && !perms.selected && !perms.disabled)
                    {
                        perms.selected = true;
                    }
                    */
                }
            }
        }
        //b2bPermission.Permissions = b2bPermission.Permissions.OrderBy(x => x.name);
        var response = _mapper.Map<TempB2BUserRolePermissionDTO>(b2bPermission);
        response.roleId = groups.Count == 0 ? null : group.id;
        response.roleName = groups.Count == 0 ? null : group.name;
        //response.userId = Guid.NewGuid(); //local user Id. It will be set in command
        return response;
    }

    public async Task<KeycloackDataResponse<bool>> AssignB2BUserGroupsAndPermissionsAsync(SetB2BUserGroupPermissionsCommand request, string realm, CancellationToken cancellationToken)
    {
        /*
         İlk olarak, kullanıcıya atanan tüm 'group' çekilir. 
         Bir kullanıcı üzerinde atanmış max 1 'group' olabilir. group atanmamış da olabiir!
         request ile gönderilen 'group' daha önceden kullanıcıya atanmamış ise atanır. birden fazla atanmış 'group' silinir.
         İkinci olarak,client üzerindeki tüm yetkiler ve kullanıcıya atanmış/atanacak olan grubun tüm yetkileri listelenir.  
         Gruba atanmış yetkiler 'selected' ve 'disabled' olarak listelenir.
         fazladan yetki ataması yapılmak istendiğinde kullanıcıya ayrıca yetki ataması ve çıkartılması yapılabilecek
         */

        var userAssignedGroups = await GetUserGroups(realm, request.userId.ToString(), cancellationToken);

        var surplusUserAssignedGroups = userAssignedGroups.Where(x => x.id != request.roleId); //her user sadece 1 group alabilir.

        foreach (var surplusUserAssignedGroup in surplusUserAssignedGroups)
        {//To unassign the role that was assigned to the user as an additional role or removed during the process of assigning the user to a group.
            await UnAssignGroupToUser(realm, surplusUserAssignedGroup.id, request.userId.ToString(), cancellationToken);
        }

        var currentUserAssignedGroups = userAssignedGroups.Where(x => x.id == request.roleId); //Each user can only join one group.
        if (currentUserAssignedGroups.Count() == 0)
        {
            if (!string.IsNullOrEmpty(request.roleId))
            {

                await AssignGroupToUser(realm, request.roleId, request.userId.ToString(), cancellationToken);
            }
        }

        var groupPerms = await GetGroupRolePermissions(request.roleId, realm, KeycloakConstants.ecommerceClient, cancellationToken);


        var ecommerceClientDetails = await GetCurrentClients(realm, KeycloakConstants.ecommerceClient, cancellationToken);
        var ecommerClientsId = ecommerceClientDetails.FirstOrDefault().id;


        var assignedUserPerms = await GetUserClientPermissionsAsync<KeycloakRoleModel>(realm, request.userId.ToString(), ecommerClientsId, cancellationToken);

        var deletedPerms = new List<KeycloakRoleModel>();
        var addedPerms = new List<KeycloakRoleModel>();

        var currentGroupDetails = groupPerms.FirstOrDefault(); //allgroupPerms
       // currentGroupDetails.Permissions.ToList().ForEach(x => x.disabled = !x.selected ? false : true);
       // var enabledPerms = currentGroupDetails.Permissions.Where(x => !x.disabled);
        var enabledPerms = currentGroupDetails.Permissions;
        if (request.Permissions != null)
        {
            foreach (var perms in request.Permissions)
            {
                if (enabledPerms.Where(x => x.id == perms.id).Any() && !assignedUserPerms.Where(x => x.id == perms.id).Any())
                {
                    addedPerms.Add(new KeycloakRoleModel() { name = perms.name, id = perms.id });
                }

            }
            foreach (var assignedUserPerm in assignedUserPerms)
            {
                /*if (!request.Permissions.Where(x => x.id == assignedUserPerm.id).Any() || currentGroupDetails.Permissions.Where(x => x.id == assignedUserPerm.id && x.disabled && x.selected).Any())
                {
                    deletedPerms.Add(new KeycloakRoleModel() { name = assignedUserPerm.name, id = assignedUserPerm.id });
                }
                */
            }
        }



        var responseAdd = await SendClientRoleMappingForUserAsync(realm, request.userId.ToString(), ecommerceClientDetails.FirstOrDefault().id, HttpMethod.Post, addedPerms, cancellationToken);
        var responseDelete = await SendClientRoleMappingForUserAsync(realm, request.userId.ToString(), ecommerceClientDetails.FirstOrDefault().id, HttpMethod.Delete, deletedPerms, cancellationToken);

        return new KeycloackDataResponse<bool>() { Data = responseAdd.Data && responseDelete.Data };



    }
    private async Task<KeycloackDataResponse<bool>> SendClientRoleMappingForUserAsync(string realm, string userId, string clientId, HttpMethod httpMethod, IEnumerable<KeycloakRoleModel> roleList, CancellationToken cancellationToken)
    {
        var url = $"admin/realms/{realm}/users/{userId}/role-mappings/clients/{clientId}";

        var stringContent = new StringContent(JsonSerializer.Serialize(roleList), Encoding.UTF8, "application/json");
        var response = await SendRequestToKeycloakAsync(url, realm, httpMethod, stringContent, cancellationToken);

        return new KeycloackDataResponse<bool>() { Data = response.IsSuccessStatusCode };

    }
    public async Task<KeycloackDataResponse<bool>> AssignGroupToUser(string realm, string groupId, string userId, CancellationToken cancellationToken)
    {
        await GenerateTokenAsync(cancellationToken);
        var endpoind = $"admin/realms/{realm}/users/{userId}/groups/{groupId}";

        var response = await SendRequestToKeycloakAsync(endpoind, realm, HttpMethod.Put, null, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new ApiException(GroupRoleConstants.ErrorWhenDeleted);
        return new KeycloackDataResponse<bool>() { Data = response.IsSuccessStatusCode };

    }
    public async Task<KeycloackDataResponse<bool>> UnAssignGroupToUser(string realm, string groupId, string userId, CancellationToken cancellationToken)
    {
        await GenerateTokenAsync(cancellationToken);
        var endpoind = $"admin/realms/{realm}/users/{userId}/groups/{groupId}";

        var response = await SendRequestToKeycloakAsync(endpoind, realm, HttpMethod.Delete, null, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new ApiException(GroupRoleConstants.ErrorWhenDeleted);
        return new KeycloackDataResponse<bool>() { Data = response.IsSuccessStatusCode };

    }

    private async Task<HttpResponseMessage> SendRequestToKeycloakAsync(string endpoind, string realm, HttpMethod httpMethod, StringContent? stringContent, CancellationToken cancellationToken)
    {
        await GenerateTokenAsync(cancellationToken);

        var reqMessage = new HttpRequestMessage()
        {
            Content = stringContent,
            Method = httpMethod,
            RequestUri = new Uri(Client.BaseAddress + endpoind),
        };
        var response = await SendAsync(reqMessage, cancellationToken);
        return response;

    }

    public async Task<ICollection<ClientPermissionModel>> GetClientPermissionsAsync(string realm, string roleName, CancellationToken cancellationToken)
    {/*role client permissions*/
        var currentClients = await GetCurrentClients(realm, KeycloakConstants.clientPrefix, cancellationToken);


        var permissions = new List<ClientPermissionModel>();


        foreach (var client in currentClients)
        {
            var clientPermissions = await GetClientRoleMappingsAsync(realm, client.id, cancellationToken);

            if (clientPermissions.Count > 0)
            {
                var orderedClientPermissions = clientPermissions.ToList().OrderBy(x => x.name);
                permissions.Add(new ClientPermissionModel() { application = client.clientId, Permissions = orderedClientPermissions });
            }
        }

        return permissions;
    }

    public async Task<List<ClientPermissionModel>> GetUnAssignedClientPermissionsAsync(string realm, string client, string identityGroupRefId, CancellationToken cancellationToken)
    {
        var clientPermissions = await GetGroupRolePermissions(identityGroupRefId, realm, KeycloakConstants.ecommerceClient, cancellationToken);

        var unAssignedClientaAndPermissions = new List<ClientPermissionModel>();
        foreach (var clientPermission in clientPermissions)//şuanki yapı ile tek client. sonradan değişirse diye
        {
            var unAssignedClientPermissions = new List<PermissionModel>();
            foreach (var permission in clientPermission.Permissions)
            {
               /* if (!permission.selected)
                {
                    unAssignedClientPermissions.Add(
                        new PermissionModel()
                        {
                            id = permission.id,
                            name = permission.name,
                            description = permission.description
                        });
                }
                */
            }
            unAssignedClientaAndPermissions.Add(new ClientPermissionModel()
            {
                application = clientPermission.application,
                containerId = clientPermission.containerId,
               // Permissions = unAssignedClientPermissions
            });
        }
        return unAssignedClientaAndPermissions;
    }

  
}
