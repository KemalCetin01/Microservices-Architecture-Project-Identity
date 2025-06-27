using MS.Services.Identity.Application.Handlers.B2bRoles.DTOs;

namespace MS.Services.Identity.Application.DTOs.Business.Request;

public class UpdateBusinessPermissionsRequestDto
{
    public Guid BusinessId { get; set; }
    public List<UpdateBusinessPermissionRequestDto> Permissions { get; set; } = null!;
}

public class UpdateBusinessPermissionRequestDto
{
    public string IdentityRefId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool Selected { get; set; } = false;
}
