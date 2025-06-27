using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

public class KeycloackRoleErrorModel : IRestErrorResponse
{
    public string? Error { get; set; }
}