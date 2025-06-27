using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.EmployeeRoles.DTOs;
public class EmployeeRolePermissionsDTO : IResponse
{
    public string application { get; set; }
    public string containerId { get; set; }
    public IEnumerable<PermissionDTO>? Permissions { get; set; }
}
