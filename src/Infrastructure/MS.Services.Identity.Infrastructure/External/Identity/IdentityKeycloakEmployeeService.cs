using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Identity.Application.Core.Infrastructure.External;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.DTOs.Identity.Request;
using MS.Services.Identity.Application.DTOs.Identity.Response;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

namespace MS.Services.Identity.Infrastructure.External.Identity;

public class IdentityKeycloakEmployeeService : IdentityKeycloakBaseService, IIdentityEmployeeService
{
    private readonly KeycloakOptions _keycloakOptions;

    public IdentityKeycloakEmployeeService(IOptions<KeycloakOptions> options,
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

    protected override string Realm { get => _keycloakOptions.microservice_realm; }

    public async Task<GetRolePermissionsResponseDto> GetRolePermissions(string roleName, CancellationToken cancellationToken)
    {
        List<ClientPermissionModel> permissionModels = await _keycloakClientService.GetPermissionsAsync(Realm, roleName, cancellationToken);
        return _mapper.Map<GetRolePermissionsResponseDto>(permissionModels);
    }

    public async Task<bool> UpdateRolePermissions(UpdateIdentityRolePermissionsRequestDto request, CancellationToken cancellationToken)
    {
        return await _keycloakRoleService.SetRolePermissions(Realm, request.RoleName, request.PermissionIds, cancellationToken);
    }
}