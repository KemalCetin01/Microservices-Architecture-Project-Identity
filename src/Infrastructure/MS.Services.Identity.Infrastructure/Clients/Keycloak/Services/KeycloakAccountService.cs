using Microsoft.Extensions.Options;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Domain.Exceptions;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
using System.Text;
using System.Text.Json;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Services;

public class KeycloakAccountService : KeycloakBaseService, IKeycloakAccountService
{
    private readonly IKeycloakRoleService _keycloakRoleService;
    private readonly KeycloakOptions _keycloakOptions;
    public KeycloakAccountService(IOptions<KeycloakOptions> options, HttpClient client, IKeycloakRoleService keycloakRoleService, string? baseAddress = null, Dictionary<string, string>? requestHeaders = null) : base(options, client, baseAddress, requestHeaders)
    {
        _keycloakRoleService = keycloakRoleService;
        SetRequestHeaders(new Dictionary<string, string> { { "Accept", "application/json" } });
        _keycloakOptions = options.Value;
    }
    private async Task<string> SetLoginClientCredentialsContent(KeycloakLoginModel keycloakLoginModel)
    {
        var collection = new List<KeyValuePair<string, string>>();
        collection.Add(new("username", keycloakLoginModel.Email));
        collection.Add(new("password", keycloakLoginModel.Password));
        collection.Add(new("client_id", keycloakLoginModel.ClientId));
        collection.Add(new("client_secret", keycloakLoginModel.ClientSecret));
        collection.Add(new("grant_type", keycloakLoginModel.GrantType));
        collection.Add(new("scope", keycloakLoginModel.Scope));

        var content = new FormUrlEncodedContent(collection);
        return await content.ReadAsStringAsync();
    }
    private async Task<string> SetLoginClientCredentialsContentWtihRefreshToken(RefreshTokenLoginModel refreshTokenLoginModel)
    {
        var collection = new List<KeyValuePair<string, string>>();
        collection.Add(new("client_id", refreshTokenLoginModel.ClientId));
        collection.Add(new("client_secret", refreshTokenLoginModel.ClientSecret));
        collection.Add(new("grant_type", refreshTokenLoginModel.GrantType));
        collection.Add(new("refresh_token", refreshTokenLoginModel.RefreshToken));

        var content = new FormUrlEncodedContent(collection);
        return await content.ReadAsStringAsync();
    }
    public async Task<TokenModel> LoginAsync(KeycloakLoginModel keycloakLoginModel, CancellationToken cancellationToken)
    {
        var endpoind = $"realms/{keycloakLoginModel.Realm}/protocol/openid-connect/token";

        string content = await SetLoginClientCredentialsContent(keycloakLoginModel);
        var stringContent = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded");
        var reqMessage = new HttpRequestMessage()
        {
            Content = stringContent,
            Method = HttpMethod.Post,
            RequestUri = new Uri(_keycloakOptions.base_address + "realms/" + keycloakLoginModel.Realm + "/protocol/openid-connect/token"),
        };
        reqMessage.Headers.Add("Accept", "application/x-www-form-urlencoded");

        var response = await SendAsync(reqMessage);
        var data = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<TokenModel>(data);

        var errorModel = JsonSerializer.Deserialize<ErrorModel>(data);
        var errorMessage = string.Format(UserStatusCodes.KeycloakLoginError.Message, errorModel.error, errorModel.error_description);

        throw new ApiException(errorMessage, UserStatusCodes.KeycloakLoginError.StatusCode);
    }

    public async Task<TokenModel> RefreshTokenLoginAsync(RefreshTokenLoginModel refreshTokenLoginModel, CancellationToken cancellationToken)
    {
        var endpoind = $"realms/{refreshTokenLoginModel.Realm}/protocol/openid-connect/token";

        string content = await SetLoginClientCredentialsContentWtihRefreshToken(refreshTokenLoginModel);
        var stringContent = new StringContent(content, Encoding.UTF8, "application/x-www-form-urlencoded");
        var reqMessage = new HttpRequestMessage()
        {
            Content = stringContent,
            Method = HttpMethod.Post,
            RequestUri = new Uri(_keycloakOptions.base_address + "realms/" + refreshTokenLoginModel.Realm + "/protocol/openid-connect/token"),
        };
        reqMessage.Headers.Add("Accept", "application/x-www-form-urlencoded");

        var response = await SendAsync(reqMessage);
        var data = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<TokenModel>(data);

        var errorModel = JsonSerializer.Deserialize<ErrorModel>(data);
        var errorMessage = string.Format(UserStatusCodes.KeycloakLoginError.Message, errorModel.error, errorModel.error_description);

        throw new ApiException(errorMessage, UserStatusCodes.KeycloakLoginError.StatusCode);
    }
}
