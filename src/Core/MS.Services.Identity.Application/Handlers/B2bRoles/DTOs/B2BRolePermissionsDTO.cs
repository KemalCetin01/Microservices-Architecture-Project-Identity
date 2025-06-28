using MS.Services.Core.Base.Dtos;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.DTOs;

namespace MS.Services.Identity.Application.Handlers.B2bRoles.DTOs;

public class B2BRolePermissionsDTO : IResponse
{
    public string application { get; set; }
    public string clientId { get; set; }

    public IEnumerable<PermissionDTO>? Permissions { get; set; }
}
