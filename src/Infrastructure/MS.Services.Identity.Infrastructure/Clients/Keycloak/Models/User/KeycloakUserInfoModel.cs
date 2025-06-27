using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models
{
    public class KeycloakUserInfoModel : IResponse
    {

        public string? Name { get; set; }
        public string? Email { get; set; }
        public Dictionary<string, ResourceRole>? ResourceAccess { get; set; }
    }

    public class ResourceRole
    {
        public List<string> roles { get; set; }
    }

}
