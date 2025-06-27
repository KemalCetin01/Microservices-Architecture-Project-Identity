using Newtonsoft.Json;
using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models
{
    public class KeycloakUserModel:IRestResponse
    {
        public string id { get; set; }
        public long createdTimestamp { get; set; }
        public string username { get; set; }
        public bool? enabled { get; set; }
        [JsonProperty("firstName")]
        public string firstName { get; set; } = null!;
        [JsonProperty("lastName")]
        public string lastName { get; set; } = null!;
        [JsonProperty("email")]
        public string email { get; set; } = null!;
        public Dictionary<string, IEnumerable<string>> attributes { get; set; }
        public IEnumerable<CredentialsModel> credentials { get; set; }
    }
}
