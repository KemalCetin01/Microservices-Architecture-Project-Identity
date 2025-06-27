namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

public class RefreshTokenLoginModel
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string GrantType { get; set; }
    public string RefreshToken { get; set; }
    public string Realm { get; set; }

}
