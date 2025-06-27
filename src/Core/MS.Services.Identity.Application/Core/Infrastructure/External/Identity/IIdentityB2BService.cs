using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Identity.Application.DTOs.Identity.Request;
using MS.Services.Identity.Application.DTOs.Identity.Response;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

namespace MS.Services.Identity.Application.Core.Infrastructure.External.Identity;

public interface IIdentityB2BService : IIdentityBaseService
{
    Task<CreateIdentityBusinessResponseDto> CreateBusinessAsync(CreateIdentityBusinessRequestDto request, CancellationToken cancellationToken);
    Task<bool> DeleteBusinessAsync(Guid identityBusinessRefId, CancellationToken cancellationToken);
    Task<List<GetBusinessRoleResponseDto>> GetBusinessRolesAsync(Guid identityBusinessRefId, CancellationToken cancellationToken);
    Task<GetBusinessRoleResponseDto> GetBusinessRoleByIdAsync(Guid identityRoleRefId, CancellationToken cancellationToken);
    Task<bool> CreateBusinessRoleAsync(Guid identityBusinessRefId, CreateBusinessRoleRequestDto request, CancellationToken cancellationToken);
    Task<bool> UpdateBusinessRoleAsync(UpdateBusinessRoleRequestDto request, CancellationToken cancellationToken);
    Task<bool> DeleteBusinessRoleAsync(Guid identityRoleRefId, CancellationToken cancellationToken);
    Task<bool> UpdateBusinessRolePermissionsAsync(Guid identityBusinessRefId, UpdateBusinessRolePermissionsRequestDto request, CancellationToken cancellationToken);
    Task<List<GetBusinessPermissionsResponseDto>> GetBusinessRolePermissionsAsync(Guid identityBusinessRefId, Guid identityRoleRefId, CancellationToken cancellationToken);
    Task<List<GetBusinessPermissionsResponseDto>> GetBusinessPermissionsAsync(Guid identityBusinessRefId, CancellationToken cancellationToken);
    Task<bool> UpdateBusinessPermissionsAsync(Guid identityBusinessRefId, List<UpdateBusinessPermissionRequestDto> permissionlist, CancellationToken cancellationToken);
    Task<AuthenticationDTO> LoginAsync(B2BLoginDTO request, CancellationToken cancellationToken);
    Task<AuthenticationDTO> RefreshTokenLoginAsync(string refreshToken, CancellationToken cancellationToken);
    Task<GetB2BUserRoleAndPermissionsResponseDto> GetB2BUserRoleAndPermissions(Guid identityUserRefId, CancellationToken cancellationToken);

}