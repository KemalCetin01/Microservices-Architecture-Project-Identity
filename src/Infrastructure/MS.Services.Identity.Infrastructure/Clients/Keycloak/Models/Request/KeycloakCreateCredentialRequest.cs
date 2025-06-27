namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models.Request;
public class KeycloakCreateCredentialRequest
{
    public bool? temporary { get; set; }
    public string? type { get; set; }
    public string value { get; set; } = null!;
}
