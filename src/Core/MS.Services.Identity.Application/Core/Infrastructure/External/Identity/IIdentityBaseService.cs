using MS.Services.Identity.Application.DTOs.Identity.Request;
using MS.Services.Identity.Application.DTOs.Identity.Response;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.Commands;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

namespace MS.Services.Identity.Application.Core.Infrastructure.External.Identity;

public interface IIdentityBaseService
{
    Task<CreateIdentityUserResponseDto> CreateUserAsync(CreateIdentityUserRequestDto request, CancellationToken cancellationToken);

    Task<UpdateIdentityUserResponseDto> UpdateUserAsync(UpdateIdentityUserRequestDto request, CancellationToken cancellationToken);

    Task<bool> DeleteUserAsync(Guid identityRefId, CancellationToken cancellationToken);

    Task<bool> UpdateUserPasswordAsync(UpdateIdentityUserPasswordRequestDto request, CancellationToken cancellationToken);

    Task<bool> UpdateUserRolesAsync(UpdateIdentityUserRolesRequestDto request, CancellationToken cancellationToken);

    Task<bool> CreateRoleAsync(CreateIdentityRoleRequestDto request, CancellationToken cancellationToken);

    Task<bool> UpdateRoleAsync(UpdateIdentityRoleRequestDto request, CancellationToken cancellationToken);

    Task<bool> DeleteRoleAsync(string roleName, CancellationToken cancellationToken);
    
}