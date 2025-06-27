using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.Networking.Http.Models;
using MS.Services.Identity.Application.Handlers.B2bRoles.Commands;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.Commands;
using MS.Services.Identity.Application.Handlers.Keycloak.Commands;
using MS.Services.Identity.Application.Helpers;
using System.Collections;
using System.Data;
using System.Text;
using System.Text.Json;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Handlers.B2bRoles.DTOs;
using static MS.Services.Identity.Application.Constants.Constants;
using System.Threading;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using System.Linq;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Services;

public class KeycloakRoleService : KeycloakBaseService, IKeycloakRoleService
{
    private readonly IUserB2BRepository _userB2BRepository;
    private readonly IMapper _mapper;

    public KeycloakRoleService(IOptions<KeycloakOptions> options, HttpClient client, IMapper mapper, IUserB2BRepository userB2BRepository,
        string baseAddress = null, Dictionary<string, string> requestHeaders = null) : base(options, client,
        baseAddress, requestHeaders)
    {
        _mapper = mapper;
        _userB2BRepository = userB2BRepository;
    }

    #region realm-level roles and composites

    private async Task<List<TResponse>> GetListAsync<TResponse>(string realm, string endpoind,
        CancellationToken cancellationToken)
        where TResponse : IRestResponse
    {
        await GenerateTokenAsync(cancellationToken);
        var result = await GetListAsync<TResponse>(endpoind,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }, cancellationToken);
        return result;
    }

    public async Task<List<TResponse>> GetRealmRolesAndCompositesAsyncTest<TResponse>(string realm, string endpoind,
        CancellationToken cancellationToken)
        where TResponse : IRestResponse
    {
        await GenerateTokenAsync(cancellationToken);
        var result = await GetListAsync<TResponse, KeycloackRoleErrorModel>(endpoind,
            new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }, cancellationToken);

        if (result == null) throw new ApiException("");
        if (result.IsSuccessStatusCode && result.Response != null) return result.Response.ToList();
        throw new ApiException(result.Error?.Error ?? "Bilinmeyen bir hata meydana geldi");

    }

    public async Task<List<RoleRepresentation>> GetRealmRolesAsync(string realm,
        CancellationToken cancellationToken)
    {
        var endpoind = $"admin/realms/{realm}/roles";

        return await GetRealmRolesAndCompositesAsyncTest<RoleRepresentation>(realm, endpoind, cancellationToken);
    }

    public async Task<List<RoleRepresentation>> GetRealmRoleCompositesByNameAsync(string realm, string roleName,
        CancellationToken cancellationToken)
    {
        var endpoind = $"admin/realms/{realm}/roles/{roleName}/composites";

        return await GetListAsync<RoleRepresentation>(realm, endpoind, cancellationToken);
    }

    public async Task<List<RoleRepresentation>> GetRealmRoleCompositesByIdAsync(string realm, string id,
        CancellationToken cancellationToken)
    {
        var endpoind = $"admin/realms/{realm}/roles-by-id/{id}/composites";

        return await GetListAsync<RoleRepresentation>(realm, endpoind, cancellationToken);
    }

    public async Task<bool> CreateRoleAsync(string realm, RoleRepresentation role,
        CancellationToken cancellationToken)
    {
        var url = $"admin/realms/{realm}/roles";
        var stringContent = new StringContent(JsonSerializer.Serialize(role), Encoding.UTF8, "application/json");

        var response = await SendKeycloackAsync(url, HttpMethod.Post, stringContent, cancellationToken);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteRoleAsync(string realm, string roleName,
        CancellationToken cancellationToken)
    {
        var url = $"admin/realms/{realm}/roles/{roleName}";

        await GenerateTokenAsync(cancellationToken);

        var reqMessage = new HttpRequestMessage()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(Client.BaseAddress + url),
        };
        var response = await SendAsync(reqMessage, cancellationToken);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> UpdateRoleAsync(string realm, RoleRepresentation roleModel,
        CancellationToken cancellationToken)
    {
        var url = $"admin/realms/{realm}/roles/{roleModel.name}";
        var stringContent = new StringContent(JsonSerializer.Serialize(roleModel), Encoding.UTF8, "application/json");

        var response = await SendKeycloackAsync(url, HttpMethod.Put, stringContent, cancellationToken);

        return response.IsSuccessStatusCode;
    }

    #endregion

    #region realm-level Associated Role (permissions)

    public async Task<KeycloackDataResponse<bool>> AddAssociatedRoleAsync(string realm, string roleId,
        IEnumerable<RoleRepresentation> role, CancellationToken cancellationToken)
    {
        var endpoind = $"admin/realms/{realm}/roles/{roleId}/composites";
        var stringContent = new StringContent(JsonSerializer.Serialize(role), Encoding.UTF8, "application/json");

        var response = await SendKeycloackAsync(endpoind, HttpMethod.Post, stringContent, cancellationToken);

        return new KeycloackDataResponse<bool>() { Data = response.IsSuccessStatusCode };
    }

    public async Task<KeycloackDataResponse<bool>> DeleteAssociatedRoleAsync(string realm, string roleName,
        IEnumerable<RoleRepresentation> role, CancellationToken cancellationToken)
    {
        var endpoind = $"admin/realms/{realm}/roles/{roleName}/composites";
        var stringContent = new StringContent(JsonSerializer.Serialize(role), Encoding.UTF8, "application/json");

        var response = await SendKeycloackAsync(endpoind, HttpMethod.Delete, stringContent, cancellationToken);

        return new KeycloackDataResponse<bool>() { Data = response.IsSuccessStatusCode };
    }

    public async Task<bool> SetRolePermissions(string realm, string roleName, List<string> permissionIds, CancellationToken cancellationToken)
    {
        var assignedRoles = await GetRealmRoleCompositesByNameAsync(realm, roleName, cancellationToken);


        var rolesToDelete = await RolesToDeleted(assignedRoles, permissionIds);
        var rolesToAdd = await RolesToAdded(assignedRoles, permissionIds);

        var responseRolesToAdd = new KeycloackDataResponse<bool>() { Data = true };
        var responseRolesToDelete = new KeycloackDataResponse<bool>() { Data = true };

        if (rolesToAdd.Count > 0)
        {
            responseRolesToAdd = await AddAssociatedRoleAsync(realm, roleName, rolesToAdd, cancellationToken);
        }

        if (rolesToDelete.Count > 0)
        {
            responseRolesToDelete = await DeleteAssociatedRoleAsync(realm, roleName, rolesToDelete, cancellationToken);
        }

        return responseRolesToAdd.Data && responseRolesToDelete.Data;
    }

    #endregion

    private async Task<HttpResponseMessage?> SendKeycloackAsync(string url, HttpMethod httpMethod,
        StringContent stringContent, CancellationToken cancellationToken)
    {
        await GenerateTokenAsync(cancellationToken);

        var reqMessage = new HttpRequestMessage()
        {
            Content = stringContent,
            Method = httpMethod,
            RequestUri = new Uri(Client.BaseAddress + url),
        };
        var response = await SendAsync(reqMessage, cancellationToken);
        return response;
    }

    #region group roles
    public async Task<KeycloackDataResponse<bool>> AssignOrUnAssignGroupPermissionsAsync(SetGroupPermissionsCommandDTO request, string realm, HttpMethod httpMethod, CancellationToken cancellationToken)
    {
        var groupRoles = new List<RoleRepresentation>();
        var groupPermsModel = _mapper.Map<ICollection<RoleRepresentation>>(request.Permissions);
        var processPerms = await GroupRolesToAdded(groupRoles, groupPermsModel);

        var response = new KeycloackDataResponse<bool>() { Data = true };

        var containerIdsInAdd = processPerms.Select(x => x.containerId).Distinct().ToList();

        foreach (var containerId in containerIdsInAdd)
        {
            if (processPerms.Count > 0)
            {
                var addGroupRoleMappingsArr =
                    new ArrayList(processPerms.Where(x => x.containerId == containerId).ToList());
                response = await AddOrDeleteClientRoleToGroup(httpMethod, realm, request.id.ToString(),
                    containerId, addGroupRoleMappingsArr, cancellationToken);
            }
        }

        return new KeycloackDataResponse<bool>() { Data = (response.Data) };
    }
    public async Task<KeycloackDataResponse<bool>> SetGroupRolePermissions(SetGroupPermissionsCommandDTO request,
        string realm, CancellationToken cancellationToken)
    {
        var userKeycloakIdList = (await _userB2BRepository.GetUsersByBusinessKeycloakId(request.id, cancellationToken))
                            ?.Select(user => user.User.IdentityRefId.ToString())
                            .ToList();

        var assignedRoles = await GetChildGroupRoleCompositesByIdAsync(realm, request.id.ToString(), cancellationToken);

        var groupRoles = new List<RoleRepresentation>();
        if (assignedRoles.clientMappings != null)
        {

            foreach (var clients in assignedRoles.clientMappings)
            {
                foreach (var item in clients.Value.mappings)
                {
                    groupRoles.Add(item);
                }
            }
        }
        var groupPermsModel = _mapper.Map<ICollection<RoleRepresentation>>(request.Permissions);
        var rolesToAdd = await GroupRolesToAdded(groupRoles, groupPermsModel);
        var rolesToDelete = await GroupRolesToDeleted(groupRoles, groupPermsModel, realm, userKeycloakIdList, cancellationToken);
        var responseRolesToAdd = new KeycloackDataResponse<bool>() { Data = true };

        var containerIdsInAdd = rolesToAdd.Select(x => x.containerId).Distinct().ToList();
        foreach (var containerId in containerIdsInAdd)
        {
            if (rolesToAdd.Count > 0)
            {
                var addGroupRoleMappingsArr =
                    new ArrayList(rolesToAdd.Where(x => x.containerId == containerId).ToList());
                responseRolesToAdd = await AddOrDeleteClientRoleToGroup(HttpMethod.Post, realm, request.id.ToString(),
                    containerId, addGroupRoleMappingsArr, cancellationToken);
            }
        }

        var responseRolesToDelete = new KeycloackDataResponse<bool>() { Data = true };
        var containerIdsInDelete = rolesToDelete.Select(x => x.containerId).Distinct().ToList();

        foreach (var containerId in containerIdsInDelete)
        {
            if (rolesToDelete.Count > 0)
            {
                var addGroupRoleMappingsArr =
                    new ArrayList(rolesToDelete.Where(x => x.containerId == containerId).ToList());
                responseRolesToDelete = await AddOrDeleteClientRoleToGroup(HttpMethod.Delete, realm,
                    request.id.ToString(), containerId, addGroupRoleMappingsArr, cancellationToken);
            }
        }

        return new KeycloackDataResponse<bool>() { Data = (responseRolesToAdd.Data && responseRolesToDelete.Data) };
    }

    private async Task<KeycloackDataResponse<bool>> AddOrDeleteClientRoleToGroup(HttpMethod httpMethod, string realm,
        string groupId, string contaionerId, ArrayList roleList, CancellationToken cancellationToken)
    {
        var endpoind = $"admin/realms/{realm}/groups/{groupId}/role-mappings/clients/{contaionerId}";

        var stringContent = new StringContent(JsonSerializer.Serialize(roleList), Encoding.UTF8, "application/json");

        var response = await SendKeycloackAsync(endpoind, httpMethod, stringContent, cancellationToken);
        if (!response.IsSuccessStatusCode)
            throw new ApiException(GroupRoleConstants.KeycloakError);

        return new KeycloackDataResponse<bool>() { Data = response.IsSuccessStatusCode };
    }

    private async Task<List<RoleRepresentation>> GroupRolesToAdded(List<RoleRepresentation> assignedRoles,
        IEnumerable<RoleRepresentation> permissions)
    {
        var rolesToAdd = new List<RoleRepresentation>();

        foreach (var permission in permissions)
        {
            if (!assignedRoles.Any(x => x.id == permission.id))
            {
                rolesToAdd.Add(new RoleRepresentation()
                { id = permission.id, name = permission.name, containerId = permission.containerId });
            }
        }

        return rolesToAdd;
    }

    private async Task<List<RoleRepresentation>> GroupRolesToDeleted(List<RoleRepresentation> assignedRoles,
        IEnumerable<RoleRepresentation> permissions, string realm, List<string> userKeycloakIdList, CancellationToken cancellationToken)
    {
        var rolesToDelete = new List<RoleRepresentation>();

        foreach (var assignedRole in assignedRoles)
        {
            if (!permissions.Any(x => x.id == assignedRole.id))
            {
                var usersInRole = await GetUserInClientRole(realm, assignedRole.containerId, assignedRole.name, cancellationToken);

                if (usersInRole?.Any(userInRole => userKeycloakIdList.Contains(userInRole.id) && userInRole.enabled==true) == true)
                    throw new ApiException(GroupRoleConstants.UserInRole);

                rolesToDelete.Add(assignedRole);
            }
        }

        return rolesToDelete;
    }


    private async Task<List<RoleRepresentation>> RolesToAdded(List<RoleRepresentation> assignedRoles,
        IEnumerable<string> PermissionIds)
    {
        var rolesToAdd = new List<RoleRepresentation>();

        foreach (var Id in PermissionIds)
        {
            if (!assignedRoles.Any(x => x.id == Id))
            {
                rolesToAdd.Add(new RoleRepresentation() { id = Id });
            }
        }

        return rolesToAdd;
    }

    private async Task<List<RoleRepresentation>> RolesToDeleted(List<RoleRepresentation> assignedRoles,
        IEnumerable<string> PermissionIds)
    {
        var rolesToDelete = new List<RoleRepresentation>();

        foreach (var assignedRole in assignedRoles)
        {
            if (!PermissionIds.Any(x => x == assignedRole.id))
            {
                rolesToDelete.Add(assignedRole);
            }
        }

        return rolesToDelete;
    }

    #endregion

    #region migration

    public async Task MigrationClientRoles(KeycloakMigrateClientRolesCommand command,
        CancellationToken cancellationToken)
    {
        var currentClients = await GetCurrentClients(command.fromRealm, command.fromClient, cancellationToken);

        var clientRolemappings =
            await GetClientRoleMappingsAsync(command.fromRealm, currentClients.FirstOrDefault().id, cancellationToken);
        var clientPermissionModel = new ClientPermissionModel();
        var toClient = await GetCurrentClients(command.toRealm, command.toClient, cancellationToken);

        clientPermissionModel.containerId = toClient.FirstOrDefault().id;
        clientPermissionModel.Permissions = clientRolemappings;

        var endpoind = $"admin/realms/{command.toRealm}/clients/{clientPermissionModel.containerId}/roles";


        foreach (var clientRolemapping in clientRolemappings)
        {
            var RoleRepresentation = new RoleRepresentation()
            {
                name = clientRolemapping.name,
                description = clientRolemapping.description
            };
            var stringContent = new StringContent(JsonSerializer.Serialize(RoleRepresentation), Encoding.UTF8,
                "application/json");
            var response = await SendKeycloackAsync(endpoind, HttpMethod.Post, stringContent, cancellationToken);
        }
    }

    public async Task AssignAllClientPermsToRealmRole(string realm, string clientId, string realmRoleId,
        CancellationToken cancellationToken)
    {
        var currentClients = await GetCurrentClients(realm, clientId, cancellationToken);

        var clientPerms = new List<RoleRepresentation>();

        foreach (var currentClient in currentClients)
        {
            var clientRolemappings1 = await GetClientRoleMappingsAsync(realm, currentClient.id, cancellationToken);
            clientPerms.AddRange(clientRolemappings1);
        }

        var roleListModel = _mapper.Map<List<RoleRepresentation>>(clientPerms);

        await AddAssociatedRoleAsync(realm, realmRoleId, roleListModel, cancellationToken);
    }



    #endregion
    private async Task<List<ClientPermissionModel>> GetGroupPermissionsAsync(string realm, string clientName, string identityGroupRefId, CancellationToken cancellationToken)
    {
        var currentClients = await GetCurrentClients(realm, clientName, cancellationToken);

        var assignedRoles = await GetChildGroupRoleCompositesByIdAsync(realm, identityGroupRefId, cancellationToken);
        var permissions = new List<ClientPermissionModel>();
        var groupPerms = new List<RoleRepresentation>();
        if (assignedRoles.clientMappings != null)
        {
            foreach (var clients in assignedRoles.clientMappings)
            {
                foreach (var item in clients.Value.mappings)
                {
                    groupPerms.Add(item);
                }
            }
        }
        foreach (var client in currentClients)
        {
            var orderedGroupPerms = groupPerms.OrderBy(a => a.name);
            permissions.Add(new ClientPermissionModel() { application = client.clientId, containerId = client.id, Permissions = _mapper.Map<IEnumerable<RoleRepresentation>>(orderedGroupPerms) });
        }

        return permissions;
    }
    public async Task<List<ClientPermissionModel>> GetChildGroupRolePermissions(string parentGroupId, string childGroupId, string realm, string clientName, CancellationToken cancellationToken)
    {
        var currentClients = await GetCurrentClients(realm, clientName, cancellationToken);
        var permissions = new List<ClientPermissionModel>();

        var groupRoles = new List<RoleRepresentation>();

        if (!string.IsNullOrEmpty(childGroupId))
        {
            var assignedRoles = await GetChildGroupRoleCompositesByIdAsync(realm, childGroupId, cancellationToken);
            if (assignedRoles.clientMappings != null)
            {
                foreach (var clients in assignedRoles.clientMappings) //groupRoles Dictionary geldiği için foreach yazıldı
                {
                    foreach (var item in clients.Value.mappings)
                    {
                        groupRoles.Add(item);
                    }
                }
            }
        }
        foreach (var client in currentClients)
        {
            var clientPermissions = await GetGroupPermissionsAsync(realm, clientName, parentGroupId, cancellationToken);
            foreach (var clientPermission in clientPermissions)
            {
                var permsList = clientPermission.Permissions.ToList();
                if (clientPermissions.Count > 0)
                {
                    if (groupRoles.Count() > 0)
                    {
                        //permsList.ForEach(a => a.selected = groupRoles.Any(b => b.id == a.id) ? true : false);
                    }

                    var orderedClientPermissions = permsList.OrderBy(a => a.name);
                    permissions.Add(new ClientPermissionModel() { application = client.clientId, containerId = client.id, Permissions = orderedClientPermissions });
                }
            }
        }

        return permissions;
    }


    private async Task<List<KeycloakUserModel>> GetUserInClientRole(string realm, string clientId, string roleName, CancellationToken cancellationToken)
    {
        var endpoind = $"admin/realms/{realm}/clients/{clientId}/roles/{roleName}/users";

        return await GetListAsync<KeycloakUserModel>(realm, endpoind, cancellationToken);
    }

}