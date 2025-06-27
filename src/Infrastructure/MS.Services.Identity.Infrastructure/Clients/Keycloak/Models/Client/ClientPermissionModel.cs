using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

public class ClientPermissionModel : IRestResponse
{
    public string application { get; set; }
    public string containerId { get; set; }
    public IEnumerable<RoleRepresentation>? Permissions { get; set; }

}
