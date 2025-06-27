using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Identity.Application.DTOs.Identity.Request;
using MS.Services.Identity.Application.DTOs.Identity.Response;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;
using MS.Services.Identity.Application.Handlers.User.DTOs;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Infrastructure.External.Identity;

public class IdentityKeycloakB2BService : IdentityKeycloakBaseService, IIdentityB2BService
{
    private readonly KeycloakOptions _keycloakOptions;

    public IdentityKeycloakB2BService(IOptions<KeycloakOptions> options,
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

    protected override string Realm { get => _keycloakOptions.ecommerce_b2b_realm; }

    public async Task<CreateIdentityBusinessResponseDto> CreateBusinessAsync(CreateIdentityBusinessRequestDto request, CancellationToken cancellationToken)
    {
        GroupRepresentation groupRepresentation = new GroupRepresentation
        {
            name = request.BusinessCode
        };

        KeycloakResponse resp = await _keycloakGroupService.CreateGroupAsync(Realm, groupRepresentation, cancellationToken);

        return new CreateIdentityBusinessResponseDto(){
            IdentityRefId = new Guid(resp.Id),
            IsSuccess = resp.IsSuccess
        };
    }

    public async Task<bool> DeleteBusinessAsync(Guid identityBusinessRefId, CancellationToken cancellationToken)
    {
        return await _keycloakGroupService.DeleteGroupAsync(Realm, identityBusinessRefId.ToString(), cancellationToken);
    }

    public async Task<bool> DeleteBusinessRoleAsync(Guid identityRoleRefId, CancellationToken cancellationToken)
    {
        return await _keycloakGroupService.DeleteGroupAsync(Realm, identityRoleRefId.ToString(), cancellationToken);
    }

    public async Task<GetB2BUserRoleAndPermissionsResponseDto> GetB2BUserRoleAndPermissions(Guid identityUserRefId, CancellationToken cancellationToken)
    {
        TempB2BUserRolePermissionDTO temp = await _keycloakGroupService.GetB2BGroupsAndPermissionsAsync(Realm, identityUserRefId.ToString(), cancellationToken);
        return _mapper.Map<GetB2BUserRoleAndPermissionsResponseDto>(temp);
    }
    
    public async Task<AuthenticationDTO> LoginAsync(B2BLoginDTO request, CancellationToken cancellationToken)
    {
        KeycloakLoginModel keycloakLoginModel = new KeycloakLoginModel
        {
            Email = request.Email,
            Password = request.Password,
            ClientId = _keycloakOptions.ecommerce_b2b_client_id,
            ClientSecret = _keycloakOptions.ecommerce_b2b_client_secret,
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
            ClientId = _keycloakOptions.ecommerce_b2b_client_id,
            ClientSecret = _keycloakOptions.ecommerce_b2b_client_secret,
            GrantType = _keycloakOptions.refresh_token_grant_type,
            RefreshToken = refreshToken
        };
        TokenModel token = await _keycloakAccountService.RefreshTokenLoginAsync(refreshLoginModel, cancellationToken);
        return _mapper.Map<AuthenticationDTO>(token);
    }

    public async Task<List<GetBusinessPermissionsResponseDto>> GetBusinessPermissionsAsync(Guid identityBusinessRefId, CancellationToken cancellationToken)
    {
        List<GetBusinessPermissionsResponseDto> response = new List<GetBusinessPermissionsResponseDto>();
        var businessPermissions = await _keycloakRoleMappingService.GetGroupClientRoleMappingsAsync(Realm, _keycloakOptions.ecommerce_b2b_client_ref_id, identityBusinessRefId.ToString(), cancellationToken);

        var appPermissions = await _keycloakRoleMappingService.GetClientRoleMappingsAsync(Realm, _keycloakOptions.ecommerce_b2b_client_ref_id, cancellationToken);

        if (appPermissions != null && appPermissions.Count > 0)
        {
            foreach(var appPermission in appPermissions) {
                response.Add(new GetBusinessPermissionsResponseDto{
                    IdentityRefId = appPermission.id!,
                    Name = appPermission.name!,
                    Description = appPermission.description!,
                    Selected = (businessPermissions != null && businessPermissions.Any(x => x.id == appPermission.id)) ? true : false
                });
            }
            return response.OrderBy(x => x.Name).ToList();
        }
        return response;
    }

    public async Task<List<GetBusinessPermissionsResponseDto>> GetBusinessRolePermissionsAsync(Guid identityBusinessRefId, Guid identityRoleRefId, CancellationToken cancellationToken)
    {
        List<GetBusinessPermissionsResponseDto> response = new List<GetBusinessPermissionsResponseDto>();
        var businessPermissions = await _keycloakRoleMappingService.GetGroupClientRoleMappingsAsync(Realm, _keycloakOptions.ecommerce_b2b_client_ref_id, identityBusinessRefId.ToString(), cancellationToken);

        var rolePermissions = await _keycloakRoleMappingService.GetGroupClientRoleMappingsAsync(Realm, _keycloakOptions.ecommerce_b2b_client_ref_id, identityRoleRefId.ToString(), cancellationToken);
        
        if (businessPermissions != null && businessPermissions.Count > 0)
        {
            foreach(var businessPermission in businessPermissions) {
                response.Add(new GetBusinessPermissionsResponseDto{
                    IdentityRefId = businessPermission.id!,
                    Name = businessPermission.name!,
                    Description = businessPermission.description!,
                    Selected = (rolePermissions != null && rolePermissions.Any(x => x.id == businessPermission.id)) ? true : false
                });
            }
            return response.OrderBy(x => x.Name).ToList();
        }
        return response;

    }

    public async Task<GetBusinessRoleResponseDto> GetBusinessRoleByIdAsync(Guid identityRoleRefId, CancellationToken cancellationToken)
    {
        var result = await _keycloakGroupService.GetGroupAsync(Realm, identityRoleRefId.ToString(), cancellationToken);
        if (result == null) throw new ResourceNotFoundException("Business role is not found on identity management server");
        return new GetBusinessRoleResponseDto { Name = result.name!, IdentityRefId = result.id! };
    }

    public async Task<List<GetBusinessRoleResponseDto>> GetBusinessRolesAsync(Guid identityBusinessRefId, CancellationToken cancellationToken)
    {
        GroupRepresentation? groupRepresentation = await _keycloakGroupService.GetGroupAsync(Realm, identityBusinessRefId.ToString(), cancellationToken);
        List<GetBusinessRoleResponseDto> businessResponseList = new List<GetBusinessRoleResponseDto>();
        if (groupRepresentation != null && groupRepresentation.subGroups != null && groupRepresentation.subGroups.Count > 0) 
        {
            foreach(var subGroup in groupRepresentation.subGroups)
            {
                businessResponseList.Add(new GetBusinessRoleResponseDto
                {
                    IdentityRefId = subGroup.id!,
                    Name = subGroup.name!
                });
            }
        }
        return businessResponseList;
    }


    public async Task<bool> UpdateBusinessPermissionsAsync(Guid identityBusinessRefId, List<UpdateBusinessPermissionRequestDto> permissionlist, CancellationToken cancellationToken)
    {
        var rolesToAdded = permissionlist.Where(x => x.Selected == true).ToList();
        if (rolesToAdded.Count > 0) {
            List<RoleRepresentation> convertedRoles = new List<RoleRepresentation>();
            foreach(var role in rolesToAdded) 
            {
                convertedRoles.Add(new RoleRepresentation
                {
                    id = role.IdentityRefId,
                    name = role.Name
                });
            }
            await _keycloakRoleMappingService.AddGroupClientRolesAsync(Realm, _keycloakOptions.ecommerce_b2b_client_ref_id, identityBusinessRefId.ToString(), convertedRoles, cancellationToken);
        }

        var rolesToDeleted = permissionlist.Where(x => x.Selected == false).ToList();
        if (rolesToDeleted.Count > 0) {
            List<RoleRepresentation> convertedRoles = new List<RoleRepresentation>();
            foreach(var role in rolesToDeleted) 
            {
                convertedRoles.Add(new RoleRepresentation
                {
                    id = role.IdentityRefId,
                    name = role.Name
                });
            }
            await _keycloakRoleMappingService.DeleteGroupClientRolesAsync(Realm, _keycloakOptions.ecommerce_b2b_client_ref_id, identityBusinessRefId.ToString(), convertedRoles, cancellationToken);
        }

        return true;
    }

    public async Task<bool> UpdateBusinessRolePermissionsAsync(Guid identityBusinessRefId, UpdateBusinessRolePermissionsRequestDto request, CancellationToken cancellationToken)
    {
        var groupRoles = await _keycloakRoleMappingService.GetGroupClientRoleMappingsAsync(Realm, _keycloakOptions.ecommerce_b2b_client_ref_id, identityBusinessRefId.ToString(), cancellationToken);
        
        var rolesToAdded = request.Permissions.Where(x => x.Selected == true).ToList();
        if (rolesToAdded.Count > 0) {
            List<RoleRepresentation> convertedRoles = new List<RoleRepresentation>();
            foreach(var role in rolesToAdded) 
            {
                if (groupRoles != null && groupRoles.Any(x => x.id == role.IdentityRefId)) 
                {
                    convertedRoles.Add(new RoleRepresentation
                    {
                        id = role.IdentityRefId,
                        name = role.Name
                    });
                }
            }
            await _keycloakRoleMappingService.AddGroupClientRolesAsync(Realm, _keycloakOptions.ecommerce_b2b_client_ref_id, request.RoleRefId.ToString(), convertedRoles, cancellationToken);
        }

        var rolesToDeleted = request.Permissions.Where(x => x.Selected == false).ToList();
        if (rolesToDeleted.Count > 0) {
            List<RoleRepresentation> convertedRoles = new List<RoleRepresentation>();
            foreach(var role in rolesToDeleted) 
            {
                convertedRoles.Add(new RoleRepresentation
                {
                    id = role.IdentityRefId,
                    name = role.Name
                });
            }
            await _keycloakRoleMappingService.DeleteGroupClientRolesAsync(Realm, _keycloakOptions.ecommerce_b2b_client_ref_id, request.RoleRefId.ToString(), convertedRoles, cancellationToken);
        }

        return true;
    }

    public async Task<bool> CreateBusinessRoleAsync(Guid identityBusinessRefId, CreateBusinessRoleRequestDto request, CancellationToken cancellationToken)
    {
        GroupRepresentation groupRepresentation = new GroupRepresentation
        {
            name = request.Name
        };
        return await _keycloakGroupService.CreateChildGroupAsync(Realm, identityBusinessRefId.ToString(), groupRepresentation, cancellationToken);
    }

    public async Task<bool> UpdateBusinessRoleAsync(UpdateBusinessRoleRequestDto request, CancellationToken cancellationToken)
    {
        GroupRepresentation groupRepresentation = new GroupRepresentation
        {
            name = request.Name
        };
        return await _keycloakGroupService.UpdateGroupAsync(Realm, request.RoleRefId.ToString(), groupRepresentation, cancellationToken);

    }

}