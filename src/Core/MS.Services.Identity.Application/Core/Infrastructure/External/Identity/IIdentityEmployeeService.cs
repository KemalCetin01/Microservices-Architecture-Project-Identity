using MS.Services.Identity.Application.DTOs.Identity.Request;
using MS.Services.Identity.Application.DTOs.Identity.Response;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.Commands;

namespace MS.Services.Identity.Application.Core.Infrastructure.External.Identity;

public interface IIdentityEmployeeService : IIdentityBaseService
{
    Task<bool> UpdateRolePermissions(UpdateIdentityRolePermissionsRequestDto request, CancellationToken cancellationToken);

    Task<GetRolePermissionsResponseDto> GetRolePermissions(string roleName, CancellationToken cancellationToken);
}