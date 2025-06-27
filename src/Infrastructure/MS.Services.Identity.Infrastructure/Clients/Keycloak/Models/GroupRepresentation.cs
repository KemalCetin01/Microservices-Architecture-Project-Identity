using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

public class GroupRepresentation : IRestResponse
{
    public string? id { get; set; }
    public string? name { get; set; }
    public string? path { get; set; }
    public List<GroupRepresentation>? subGroups { get; set; }
    public List<string>? realmRoles { get; set; }
    public Dictionary<string, List<string>>? clientRoles { get; set; }
    public Dictionary<string, List<string>>? attributes { get; set; }
}
