
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using AutoMapper;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.DTOs.AddressLocation.Request;
using MS.Services.Identity.Application.DTOs.AddressLocation.Response;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

namespace MS.Services.Identity.Infrastructure.Services.Addresses;

public class AuthTokenService : IAuthTokenService
{
    public AuthTokenService()
    {
    }

    public IdentityUserInfoResponseDto GetUserDetailsFromJwtToken(string token, CancellationToken cancellationToken)
    {
        var handler = new JwtSecurityTokenHandler();
        JwtSecurityToken jsonToken = handler.ReadJwtToken(token);
        var data = jsonToken.Payload;
        var resourceAccess = data.Claims.FirstOrDefault(x => x.Type == "resource_access") != null ? data.Claims.FirstOrDefault(x => x.Type == "resource_access").Value : null;
        var name = data.Claims.FirstOrDefault(x => x.Type == "name") != null ? data.Claims.FirstOrDefault(x => x.Type == "name").Value : null;
        var email = data.Claims.FirstOrDefault(x => x.Type == "email") != null ? data.Claims.FirstOrDefault(x => x.Type == "email").Value : null;
        var resourceAccessData = new Dictionary<string, IdentityResourceRoleDto>();
        if (resourceAccess != null) {
            resourceAccessData = JsonSerializer.Deserialize<Dictionary<string, IdentityResourceRoleDto>>(resourceAccess);
        }
        return new IdentityUserInfoResponseDto
        {
            Name = name,
            Email = email,
            ResourceAccess = resourceAccessData
        };
    }
}
