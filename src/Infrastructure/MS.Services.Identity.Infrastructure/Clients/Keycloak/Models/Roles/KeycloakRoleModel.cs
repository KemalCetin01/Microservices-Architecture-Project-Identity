using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
public class KeycloakRoleModel : IRestResponse
{
    public string id { get; set; }
    public string name { get; set; }
    public string? description { get; set; }
    public bool? composite { get; set; }
    public string containerId { get; set; }
    //public IDictionary<string, object> Attributes { get; set; }
    
}
