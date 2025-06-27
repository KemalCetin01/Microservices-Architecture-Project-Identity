using Microsoft.Extensions.Options;
namespace MS.Services.Identity.Application.Handlers.Keycloak.Commands;

public class CreateSuperUserAndRolesCommand
{
    public string Realm { get; set; }
}