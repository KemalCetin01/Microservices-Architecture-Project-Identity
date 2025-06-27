namespace MS.Services.Identity.Application.DTOs.Business.Request;

public class UpdateBusinessRolePermissionsRequestDto
{
    public Guid BusinessId { get; set; }
    public Guid RoleRefId { get; set; }
    public List<UpdateBusinessRolePermissionRequestDto> Permissions { get; set; } = null!;
}

public class UpdateBusinessRolePermissionRequestDto
{
    public string IdentityRefId { get; set; } = null!;
    public string Name { get; set; } = null!;
    public bool Selected { get; set; } = false;
}
