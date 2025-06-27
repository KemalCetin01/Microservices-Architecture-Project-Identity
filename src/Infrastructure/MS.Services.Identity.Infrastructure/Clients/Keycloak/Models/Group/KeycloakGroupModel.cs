using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

public class KeycloakGroupModel : IRestResponse
{
    public string id { get; set; }
    public string name { get; set; }
    public string path { get; set; }
    public bool selected { get; set; } = false;
    public IEnumerable<KeycloakGroupModel> subGroups { get; set; }
    public IEnumerable<string> realmRoles { get; set; }
    public IDictionary<string, IEnumerable<string>> clientRoles { get; set; }
    public IDictionary<string, IEnumerable<string>> attributes { get; set; }
}
