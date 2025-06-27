using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;


namespace MS.Services.Identity.Application.DTOs.Identity.Request;
public class UpdateIdentityRolePermissionsRequestDto
{
    public string RoleName { get; set; } = null!;
    public List<string> PermissionIds { get; set; } = null!;
}