using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Identity.Application.Core.Infrastructure.External;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.DTOs.Identity.Response;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2CUser;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

namespace MS.Services.Identity.Infrastructure.External.Identity;

public class IdentityKeycloakB2CService : IdentityKeycloakBaseService, IIdentityB2CService
{
    private readonly KeycloakOptions _keycloakOptions;

    public IdentityKeycloakB2CService(IOptions<KeycloakOptions> options,
        IKeycloakUserService keycloakUserService, 
        IKeycloakTokenService keycloakTokenService, 
        IKeycloakRoleService keycloakRoleService, 
        IKeycloakClientService keycloakClientService, 
        IKeycloakRoleMappingService keycloakRoleMappingService, 
        IKeycloakAccountService keycloakAccountService,
        IKeycloakGroupService keycloakGroupService,
        IMapper mapper) : base(keycloakUserService, keycloakTokenService, keycloakRoleService, keycloakClientService, keycloakRoleMappingService, keycloakAccountService, keycloakGroupService, mapper)
    {
        _keycloakOptions = options.Value;
    }

    protected override string Realm { get => _keycloakOptions.ecommerce_b2c_realm; }

    public async Task<AuthenticationDTO> LoginAsync(B2CLoginDTO request, CancellationToken cancellationToken)
    {

        KeycloakLoginModel keycloakLoginModel = new KeycloakLoginModel
        {
            Email = request.EmailOrPhone,
            Password = request.Password,
            ClientId = _keycloakOptions.ecommerce_b2c_client_id,
            ClientSecret = _keycloakOptions.ecommerce_b2c_client_secret,
            GrantType = _keycloakOptions.ecommerce_grant_type,
            Scope = _keycloakOptions.ecommerce_scope,
            Realm = Realm
        };
        TokenModel token = await _keycloakAccountService.LoginAsync(keycloakLoginModel, cancellationToken);
        return _mapper.Map<AuthenticationDTO>(token);
    }

    public async Task<AuthenticationDTO> RefreshTokenLoginAsync(string refreshToken, CancellationToken cancellationToken)
    {
        RefreshTokenLoginModel refreshLoginModel = new RefreshTokenLoginModel
        {
            Realm = Realm,
            ClientId = _keycloakOptions.ecommerce_b2c_client_id,
            ClientSecret = _keycloakOptions.ecommerce_b2c_client_secret,
            GrantType = _keycloakOptions.refresh_token_grant_type,
            RefreshToken = refreshToken
        };
        TokenModel token = await _keycloakAccountService.RefreshTokenLoginAsync(refreshLoginModel, cancellationToken);
        return _mapper.Map<AuthenticationDTO>(token);
    }

}