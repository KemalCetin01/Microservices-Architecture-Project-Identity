using Newtonsoft.Json;
using MS.Services.Core.Networking.Http.Models;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models.Base;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
public class UserRepresentation
{
    public string? id { get; set; }
    public long? createdTimestamp { get; set; }
    public bool? enabled { get; set; }
    public string? firstName { get; set; }
    public string? lastName { get; set; }
    public string? email { get; set; }
    public Dictionary<string, ICollection<string>>? attributes { get; set; }
    public ICollection<CredentialRepresentation>? credentials { get; set; }
}