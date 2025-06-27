using Microsoft.Extensions.Options;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Services;
public class KeycloakClientService : KeycloakBaseService, IKeycloakClientService
{
    private readonly IKeycloakRoleService _keycloakRoleService;
    public KeycloakClientService(IOptions<KeycloakOptions> options, HttpClient client, IKeycloakRoleService keycloakRoleService, string? baseAddress = null, Dictionary<string, string>? requestHeaders = null) : base(options, client, baseAddress, requestHeaders)
    {
        _keycloakRoleService = keycloakRoleService;
        SetRequestHeaders(new Dictionary<string, string> { { "Accept", "application/json" } });
    }



    public async Task<List<ClientPermissionModel>> GetPermissionsAsync(string realms, string roleName, CancellationToken cancellationToken)
    {
        var currentClients = await GetCurrentClients(realms, KeycloakConstants.clientPrefix, cancellationToken);

        var permissions = new List<ClientPermissionModel>();

        var realmRoleComposites = await _keycloakRoleService.GetRealmRoleCompositesByNameAsync(realms, roleName, cancellationToken);

        foreach (var client in currentClients)
        {
            var clientPermissions = await GetClientRoleMappingsAsync(realms, client.id, cancellationToken);

            if (clientPermissions?.Count > 0)
            {
                clientPermissions.ForEach(a => a.selected = realmRoleComposites.Any(b => b.id == a.id) ? true : false);
                var orderedClientPermissions = clientPermissions.ToList().OrderBy(x => x.name);
                permissions.Add(new ClientPermissionModel() { application = client.clientId, Permissions = orderedClientPermissions });
            }
        }

        return permissions;
    }



}
