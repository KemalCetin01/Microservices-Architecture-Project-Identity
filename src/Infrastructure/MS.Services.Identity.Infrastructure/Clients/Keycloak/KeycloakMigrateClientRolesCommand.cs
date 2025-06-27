namespace MS.Services.Identity.Application.Handlers.Keycloak.Commands;

public class KeycloakMigrateClientRolesCommand
{
    public string fromRealm { get; set; }
    public string fromClient { get; set; }
    public string toRealm { get; set; }
    public string toClient { get; set; }
}