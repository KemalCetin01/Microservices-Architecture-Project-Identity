
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Domain.Entities;
using System;
﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models.Base;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models.Request;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Text;
using System.Text.Json;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Services;

public class KeycloakUserService : KeycloakBaseService, IKeycloakUserService
{
    public KeycloakUserService(IOptions<KeycloakOptions> options, HttpClient client) : base(options, client)
    {
        SetRequestHeaders(new Dictionary<string, string> { { "Accept", "application/json" } });
    }

    public KeycloakUserInfoModel GetUserDetails(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jsonToken = handler.ReadJwtToken(token);
        var data = jsonToken.Payload;
        var resourceAccess = data.Claims.FirstOrDefault(x => x.Type == "resource_access") != null ? data.Claims.FirstOrDefault(x => x.Type == "resource_access").Value : null;
        var name = data.Claims.FirstOrDefault(x => x.Type == "name") != null ? data.Claims.FirstOrDefault(x => x.Type == "name").Value : null;
        var email = data.Claims.FirstOrDefault(x => x.Type == "email") != null ? data.Claims.FirstOrDefault(x => x.Type == "email").Value : null;
        var resourceAccessData = resourceAccess != null ? JsonSerializer.Deserialize<Dictionary<string, ResourceRole>>(resourceAccess) : null;
        return new KeycloakUserInfoModel
        {
            Name = name,
            Email = email,
            ResourceAccess = resourceAccessData
        };
    }
    public async Task<List<UserResponse>> GetUsersAsync(string realm, CancellationToken cancellationToken  )
    {
        var url = $"admin/realms/{realm}/users";

        await GenerateTokenAsync(cancellationToken);

        var result = await GetListAsync<UserResponse>(url, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }, cancellationToken);
        return result;
    }

    public async Task<UserResponse> GetUserById(string realm, string id, CancellationToken cancellationToken  )
    {
        var url = $"admin/realms/{realm}/users/{id}";

        await GenerateTokenAsync(cancellationToken);
        var result = await GetAsync<UserResponse>(url, cancellationToken);
        return result;
    }

    public async Task<KeycloakResponse> CreateUserAsync(string realm, UserRepresentation userRepresentation, CancellationToken cancellationToken  )
    {
        var url = $"admin/realms/{realm}/users";

        var response = await SendKeycloakAsync(url, realm, HttpMethod.Post, userRepresentation, cancellationToken);

        return response;
    }

    public async Task<bool> UpdateUserAsync(string realm, UserRepresentation userRepresentation, CancellationToken cancellationToken  )
    {
        var url = $"admin/realms/{realm}/users/{userRepresentation.id}";

        var response = await SendKeycloakAsync(url, realm, HttpMethod.Put, userRepresentation, cancellationToken);

        return response.IsSuccess;
    }

    public async Task<bool> DeleteUserAsync(string realm, string userId, CancellationToken cancellationToken)
    {
        var url = $"admin/realms/{realm}/users/{userId}";

        await GenerateTokenAsync(cancellationToken);

        var reqMessage = new HttpRequestMessage()
        {
            Method = HttpMethod.Delete,
            RequestUri = new Uri(Client.BaseAddress + url),
        };
        var response = await SendAsync(reqMessage, cancellationToken);

        return response.IsSuccessStatusCode;

    }

    public async Task<bool> SoftDeleteUserAsync(string realm, string userId, CancellationToken cancellationToken  )
    {
        var url = $"admin/realms/{realm}/users/{userId}";

        var user = new UserRepresentation()
        {
            id = userId,
            enabled = false
        };

        var response = await SendKeycloakAsync(url, realm, HttpMethod.Put, user, cancellationToken);
        return response.IsSuccess;

    }

/*
    public Task<bool> ResetUserPasswordAsync(string realm, string userId, string password, bool temporary = true)
        var response = await SendKeycloakAsync(url, realm, HttpMethod.Put, user, cancellationToken);
        return response.IsSuccess;

    }
    */
    private async Task<KeycloakResponse> SendKeycloakAsync(string url, string realm, HttpMethod httpMethod, UserRepresentation request, CancellationToken cancellationToken)
    {
        await GenerateTokenAsync(cancellationToken);

        var stringContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        var reqMessage = new HttpRequestMessage()
        {
            Content = stringContent,
            Method = httpMethod,
            RequestUri = new Uri(Client.BaseAddress + url),
        };
        var response = await SendAsync(reqMessage, cancellationToken);

        var result = new KeycloakResponse();
        result.IsSuccess = response.IsSuccessStatusCode;

        if (response.Headers.Location != null)
        {
            result.Id = response.Headers.Location.Segments.Last();
        }
        return result;
    }
    public async Task<bool> UpdateUserPasswordAsync(string realm, string userId, CredentialRepresentation credentialRepresentation, CancellationToken cancellationToken)
    {
        var url = $"admin/realms/{realm}/users/{userId}/reset-password";

        await GenerateTokenAsync(cancellationToken);

        var stringContent = new StringContent(System.Text.Json.JsonSerializer.Serialize(credentialRepresentation), Encoding.UTF8, "application/json");

        var reqMessage = new HttpRequestMessage()
        {
            Content = stringContent,
            Method = HttpMethod.Put,
            RequestUri = new Uri(Client.BaseAddress + url),
        };
        var response = await SendAsync(reqMessage, cancellationToken);
        var result = new KeycloakResponse();
        result.IsSuccess = response.IsSuccessStatusCode;

        return result.IsSuccess;

    }

}
