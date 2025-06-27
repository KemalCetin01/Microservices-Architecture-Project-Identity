namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
public class CredentialRepresentation
{
    public bool? temporary { get; set; }
    public string? type { get; set; }
    public string value { get; set; } = null!;
}
