using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

public class KeycloakBusinessGroupModel : IRestResponse
{
    public string id { get; set; }
    public string name { get; set; }
    public string path { get; set; }
}
