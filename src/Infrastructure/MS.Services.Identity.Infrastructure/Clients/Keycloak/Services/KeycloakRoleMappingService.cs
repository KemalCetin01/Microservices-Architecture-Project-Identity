using Microsoft.Extensions.Options;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
using System.Text;
using System.Text.Json;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Services;
public class KeycloakRoleMappingService : KeycloakBaseService, IKeycloakRoleMappingService
{
    public KeycloakRoleMappingService(IOptions<KeycloakOptions> options, HttpClient client, string? baseAddress = null, Dictionary<string, string>? requestHeaders = null) : base(options, client, baseAddress, requestHeaders)
    {
        SetRequestHeaders(new Dictionary<string, string> { { "Accept", "application/json" } });
    }
    #region group role mapper
    private async Task<KeycloackDataResponse<bool>> SendGroupRoleMappingForUserAsync(string realm, string groupId, HttpMethod httpMethod, List<RoleRepresentation> roleList, CancellationToken cancellationToken  )
    {
        var url = $"admin/realms/{realm}/groups/{groupId}/role-mappings/realm";

        var roleDetailList = new List<RoleRepresentation>();
        foreach (var role in roleList)
        {
            var roleDetail = await GetRoleDetailByName(realm, role.name, cancellationToken);
            roleDetailList.Add(roleDetail);
        }

        var response = await SendKeycloakAsync(url, realm, httpMethod, roleDetailList, cancellationToken);

        return new KeycloackDataResponse<bool>() { Data = response.IsSuccessStatusCode };

    }

    public async Task<KeycloackDataResponse<bool>> CreateRealmRoleMappingForGroupAsync(string realm, string groupId, List<RoleRepresentation> roleList, CancellationToken cancellationToken  )
    {
        return await SendGroupRoleMappingForUserAsync(realm, groupId, HttpMethod.Post, roleList, cancellationToken);
    }
    #endregion

    #region user role mappers
    private async Task<KeycloackDataResponse<bool>> SendRoleMappingForUserAsync(string realm, string userId, HttpMethod httpMethod, List<RoleRepresentation> roleList, CancellationToken cancellationToken  )
    {
        var url = $"admin/realms/{realm}/users/{userId}/role-mappings/realm";

        var roleDetailList = new List<RoleRepresentation>();
        foreach (var role in roleList)
        {
            var roleDetail = await GetRoleDetailByName(realm, role.name, cancellationToken);
            roleDetailList.Add(roleDetail);
        }

        var response = await SendKeycloakAsync(url, realm, httpMethod, roleDetailList, cancellationToken);

        return new KeycloackDataResponse<bool>() { Data = response.IsSuccessStatusCode };

    }
    public async Task<KeycloackDataResponse<bool>> CreateRealmRoleMappingForUserAsync(string realm, string userId, List<RoleRepresentation> roleList, CancellationToken cancellationToken  )
    {
        return await SendRoleMappingForUserAsync(realm, userId, HttpMethod.Post, roleList, cancellationToken);
    }

    public async Task<KeycloackDataResponse<bool>> DeleteRealmRoleMappingForUserAsync(string realm, string userId, List<RoleRepresentation> roleList, CancellationToken cancellationToken  )
    {
        return await SendRoleMappingForUserAsync(realm, userId, HttpMethod.Delete, roleList, cancellationToken);
    }

    public async Task<KeycloackDataResponse<bool>> UpdateRealmRoleMappingForUserAsync(string realm, string userId, List<RoleRepresentation> deletedRole, List<RoleRepresentation> newRole, CancellationToken cancellationToken  )
    {
        /*user role mappingslerde Put olmadığından dolayı unassign-assign yapılır*/
        var deleteResponse = await DeleteRealmRoleMappingForUserAsync(realm, userId, deletedRole, cancellationToken);
        if (deleteResponse.Data)
            return await SendRoleMappingForUserAsync(realm, userId, HttpMethod.Post, newRole, cancellationToken);

        return new KeycloackDataResponse<bool>() { Data = false };
    }
    #endregion

    private async Task<HttpResponseMessage> SendKeycloakAsync(string url, string realm, HttpMethod httpMethod, List<RoleRepresentation> role, CancellationToken cancellationToken  )
    {
        await GenerateTokenAsync(cancellationToken);

        var stringContent = new StringContent(JsonSerializer.Serialize(role), Encoding.UTF8, "application/json");
        var reqMessage = new HttpRequestMessage()
        {
            Content = stringContent,
            Method = httpMethod,
            RequestUri = new Uri(Client.BaseAddress + url),
        };
        var response = await SendAsync(reqMessage, cancellationToken);
        return response;

    }

    public async Task<bool> AddGroupClientRolesAsync(string realm, string clientRefId, string groupId, List<RoleRepresentation> roles, CancellationToken cancellationToken)
    {
        var endpoint = $"admin/realms/{realm}/groups/{groupId}/role-mappings/clients/{clientRefId}";

        var response = await SendKeycloakAsync(endpoint, realm, HttpMethod.Post, roles, cancellationToken);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteGroupClientRolesAsync(string realm, string clientRefId, string groupId, List<RoleRepresentation> roles, CancellationToken cancellationToken)
    {
        var endpoint = $"admin/realms/{realm}/groups/{groupId}/role-mappings/clients/{clientRefId}";

        var response = await SendKeycloakAsync(endpoint, realm, HttpMethod.Delete, roles, cancellationToken);

        return response.IsSuccessStatusCode;
    }

}
