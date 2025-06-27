using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Helpers;

public static class KeycloakHelper
{

    public static KeycloakUserModel ToKeycloakUserCreateModel(string referenceId, string email, string firstName, string lastName, string password)
    {
        return new KeycloakUserModel
        {
            createdTimestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds(),
            email = email,
            firstName = firstName,
            lastName = lastName,
            enabled = true,
            credentials = new[] { new CredentialsModel { value = password } },
            attributes = new Dictionary<string, IEnumerable<string>>
            {
                { KeycloakConstants.ODRefId, new List<string>() { referenceId } }
            }
        };
    }

    public static KeycloakUserModel ToKeycloakUserUpdateModel(string erpRefId, string email, string firstName, string lastName)
    {
        return new KeycloakUserModel
        {
            id = erpRefId,
            email = email,
            firstName = firstName,
            lastName = lastName
        };
    }

    public static KeycloakLoginModel ToKeycloakLoginModel(string email, string password, string clientId, string clientSecret, string grantType, string scope, string realm)
    {
        return new KeycloakLoginModel
        {
            ClientId = clientId,
            ClientSecret = clientSecret,
            Email = email,
            Password = password,
            GrantType = grantType,
            Scope = scope,
            Realm = realm
        };
    }
   
    public static RefreshTokenLoginModel ToRefreshTokenLoginModel(string refreshToken, string clientId, string clientSecret, string grantType, string realm)
    {
        return new RefreshTokenLoginModel
        {
            RefreshToken = refreshToken,
            ClientId = clientId,
            ClientSecret = clientSecret,
            GrantType = grantType,
            Realm = realm
        };
    }
    public static CredentialsModel ToKeycloakCredentialsModel(string password) {
        return new CredentialsModel
        {
            type = "password",
            value = password,
            temporary = false
        };
    }
    
}