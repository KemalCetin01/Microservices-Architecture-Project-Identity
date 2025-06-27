using MS.Services.Core.Networking.Http.Models;


namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

public class KeycloakResponse : IRestResponse
{
    public bool IsSuccess { get; set; }
    public string Id { get; set; } = null!;
}
