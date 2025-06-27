namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

public class KeycloakLoginModel
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ClientId { get; set; } = null!;
    public string ClientSecret { get; set; } = null!;
    public string GrantType { get; set; } = null!;
    public string Scope { get; set; } = null!;
    public string Realm { get; set; } = null!;
}
