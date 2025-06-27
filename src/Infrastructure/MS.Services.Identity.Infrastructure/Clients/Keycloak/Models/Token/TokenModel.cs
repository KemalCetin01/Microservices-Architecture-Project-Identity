using Newtonsoft.Json;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

public class TokenModel
{
    public string access_token { get; set; }
    public int expires_in { get; set; }
    public int refresh_expires_in { get; set; }
    public string token_type { get; set; }
    public string refresh_token { get; set; }
    [JsonProperty("not-before-policy")]
    public int notbeforepolicy { get; set; }
    public string scope { get; set; }
}

