
using Microsoft.Extensions.Options;
using MS.Services.Core.Networking.Http.Models;
using MS.Services.Core.Networking.Http.Services;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Services;

public class KeycloakBaseService : RestService, IKeycloakTokenService //TODO: Bunun Adı KeycloakBaseService (abstract method) olacak
{
    private static TokenModel token { get; set; }
    private static DateTime LastTokenExpiredTime { get; set; }
    private readonly KeycloakOptions _keycloakOptions;
    public KeycloakBaseService(IOptions<KeycloakOptions> options, HttpClient client, string? baseAddress = null, Dictionary<string, string>? requestHeaders = null) : base(client, baseAddress, requestHeaders)
    {
        SetRequestHeaders(new Dictionary<string, string> { { "Accept", "application/json" } });
        _keycloakOptions = options.Value;
        Client.BaseAddress = new Uri(_keycloakOptions.base_address);
    }

    public async Task GenerateTokenAsync(CancellationToken cancellationToken)
    {

        var expDate = DateTime.Now.AddMilliseconds(15);
        if (expDate > LastTokenExpiredTime)
        {
            string content = await SetClientCredentialsContent();
            var stringContent = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded");

            var reqMessage = new HttpRequestMessage()
            {
                Content = stringContent,
                Method = HttpMethod.Post,
                RequestUri = new Uri(_keycloakOptions.base_address + "realms/" + _keycloakOptions.master_realm + "/protocol/openid-connect/token"),
            };
            reqMessage.Headers.Add("Accept", "application/x-www-form-urlencoded");

            var response = await SendAsync(reqMessage);
            var data = await response.Content.ReadAsStringAsync(cancellationToken);
            token = JsonSerializer.Deserialize<TokenModel>(data);

            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token.access_token);
            var tokenS = jsonToken as JwtSecurityToken;
            var claims = tokenS.Claims;
            var exp = tokenS.Claims.First(x => x.Type == "exp").Value;
            var expTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(exp)).LocalDateTime;
            LastTokenExpiredTime = expTime;
            Client.DefaultRequestHeaders.Authorization = null;
        }

        if (Client.DefaultRequestHeaders.Authorization == null)
            SetRequestHeaders(new Dictionary<string, string> { { "Authorization", "Bearer " + token.access_token } });

    }
    private async Task<string> SetClientCredentialsContent()
    {
        var collection = new List<KeyValuePair<string, string>>();
        collection.Add(new("grant_type", _keycloakOptions.grant_type));
        collection.Add(new("client_id", _keycloakOptions.msidentity_client_id));
        collection.Add(new("client_secret", _keycloakOptions.msidentity_client_secret));

        var content = new FormUrlEncodedContent(collection);
        return await content.ReadAsStringAsync();

    }

    public async Task<RoleRepresentation> GetRoleDetailByName(string realm, string roleName, CancellationToken cancellationToken)
    {
        await GenerateTokenAsync(cancellationToken);
        var result = await GetAsync<RoleRepresentation>($"admin/realms/{realm}/roles/{roleName}", default);
        return result;
    }
    public async Task<ICollection<ClientModel>> GetClientsAsync(string realm, CancellationToken cancellationToken)
    {
        var endpoind = $"admin/realms/{realm}/clients";
        await GenerateTokenAsync(cancellationToken);

        var result = await GetListAsync<ClientModel>(endpoind, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }, cancellationToken);
        return result;
    }
    public async Task<List<RoleRepresentation>?> GetClientRoleMappingsAsync(string realm, string clientId, CancellationToken cancellationToken)
    {
        var endpoind = $"admin/realms/{realm}/clients/{clientId}/roles";
        await GenerateTokenAsync(cancellationToken);
        var result = await GetListAsync<RoleRepresentation>(endpoind, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }, cancellationToken);
        return result;
    }

    public async Task<List<ClientModel>> GetCurrentClients(string realm, string clientId, CancellationToken cancellationToken)
    {
        var clients = await GetClientsAsync(realm, cancellationToken);
        var currentClients = new List<ClientModel>();
        foreach (var client in clients)
        {
            if (client.clientId.Contains(clientId)) // Keycloak GetClientsAsync search olmadığı için bu şekilde yazıldı
            {
                currentClients.Add(client);
            }
        }

        return currentClients;
    }

    public async Task<List<RoleRepresentation>?> GetGroupClientRoleMappingsAsync(string realm, string clientRefId, string groupId, CancellationToken cancellationToken) {
        var endpoint = $"admin/realms/{realm}/groups/{groupId}/role-mappings/clients/{clientRefId}";
        await GenerateTokenAsync(cancellationToken);
        return await GetListAsync<RoleRepresentation>(endpoint, cancellationToken);
    }

    public async Task<List<ClientPermissionModel>> GetGroupRolePermissions(string id, string realm, string clientName, CancellationToken cancellationToken)
    {
        var currentClients = await GetCurrentClients(realm, clientName, cancellationToken);
        var permissions = new List<ClientPermissionModel>();

        var groupRoles = new List<RoleRepresentation>();

        if (!string.IsNullOrEmpty(id))
        {
            var assignedRoles = await GetChildGroupRoleCompositesByIdAsync(realm, id, cancellationToken);
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
            var clientPermissions = await GetClientRoleMappingsAsync(realm, client.id, cancellationToken);

            if (clientPermissions.Count > 0)
            {

                if (groupRoles.Count()>0)
                {
                   // clientPermissions.ForEach(a => a.selected = groupRoles.Any(b => b.id == a.id) ? true : false);
                }

                var orderedClientPermissions = clientPermissions.OrderBy(a => a.name);
                permissions.Add(new ClientPermissionModel() { application = client.clientId, containerId = client.id, Permissions = orderedClientPermissions });
            }
        }

        return permissions;
    }

    public async Task<KeycloakRootMappings> GetChildGroupRoleCompositesByIdAsync(string realm, string id, CancellationToken cancellationToken)
    {
        var endpoind = $"admin/realms/{realm}/groups/{id}/role-mappings";

        return await GetGroupCompositesAsync<KeycloakRootMappings>(endpoind, cancellationToken);
    }
    public async Task<GroupRepresentation?> GetGroupAsync(string realm, string groupId, CancellationToken cancellationToken)
    {
        var endpoint = $"admin/realms/{realm}/groups/{groupId}";
        await GenerateTokenAsync(cancellationToken);
        var result = await GetAsync<GroupRepresentation>(endpoint, cancellationToken);
        return result;
    }

    public async Task<TResponse> GetGroupCompositesAsync<TResponse>(string endpoind, CancellationToken cancellationToken)
 where TResponse : IRestResponse
    {
        await GenerateTokenAsync(cancellationToken);
        var result = await GetAsync<TResponse>(endpoind, cancellationToken);
        return result;
    }

    public async Task<List<TResponse>> GetUserClientPermissionsAsync<TResponse>(string realm, string userId, string clientId, CancellationToken cancellationToken)
    where TResponse : IRestResponse
    {
        var endpoind = $"admin/realms/{realm}/users/{userId}/role-mappings/clients/{clientId}";

        await GenerateTokenAsync(cancellationToken);
        var result = await GetListAsync<TResponse>(endpoind, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }, cancellationToken);
        return result;
    }
}