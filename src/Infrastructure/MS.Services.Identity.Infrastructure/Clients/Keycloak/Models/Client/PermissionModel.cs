using MS.Services.Core.Networking.Http.Models;
namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
public class PermissionModel : IRestResponse
{
    public string id { get; set; }
    public string name { get; set; }
    public string description { get; set; }
    public bool selected { get; set; }
    public bool disabled { get; set; }
}