using System.Text.Json.Serialization;
using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
public class IdentityUserInfoResponseDto : IResponse
{

    public string? Name { get; set; }
    public string? Email { get; set; }
    public Dictionary<string, IdentityResourceRoleDto>? ResourceAccess { get; set; }
}

public class IdentityResourceRoleDto
{
    [JsonPropertyName("roles")]
    public List<string>? Roles { get; set; }
}
