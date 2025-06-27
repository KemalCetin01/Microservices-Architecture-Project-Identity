using MS.Services.Core.Networking.Http.Models;
namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models
{
    public class KeycloakRootMappings : IRestResponse
    {
        public Dictionary<string, KeycloakClientMappings> clientMappings { get; set; }
    }
}
