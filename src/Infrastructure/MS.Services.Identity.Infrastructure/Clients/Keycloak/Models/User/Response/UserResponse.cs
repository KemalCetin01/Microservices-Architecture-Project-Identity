using MS.Services.Core.Networking.Http.Models;
using System.Text.Json.Serialization;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

public class UserResponse : IRestResponse
{
    [JsonPropertyName("id")]
    public Guid Id { get; set; }
    [JsonPropertyName("createdTimestamp")]
    public long CreatedTimestamp { get; set; }
    [JsonPropertyName("username")]
    public string Username { get; set; }
    [JsonPropertyName("enabled")]
    public bool Enabled { get; set; }
    [JsonPropertyName("totp")]
    public bool Totp { get; set; }
    [JsonPropertyName("emailVerified")]
    public bool EmailVerified { get; set; }
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }
    [JsonPropertyName("lastName")]
    public string LastName { get; set; }
    [JsonPropertyName("disableableCredentialTypes")]
    public string[] DisableableCredentialTypes { get; set; }
    [JsonPropertyName("requiredActions")]
    public string[] RequiredActions { get; set; }
    [JsonPropertyName("notBefore")]
    public int NotBefore { get; set; }
    [JsonPropertyName("access")]
    public Access Access { get; set; }
}

public class Access
{
    [JsonPropertyName("manageGroupMembership")]
    public bool ManageGroupMembership { get; set; }
    [JsonPropertyName("view")]
    public bool View { get; set; }
    [JsonPropertyName("mapRoles")]
    public bool MapRoles { get; set; }
    [JsonPropertyName("impersonate")]
    public bool Impersonate { get; set; }
    [JsonPropertyName("manage")]
    public bool Manage { get; set; }
    
}