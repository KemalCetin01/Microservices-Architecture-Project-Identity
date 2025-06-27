using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.DTOs.Identity.Response;

public class GetB2BUserRoleAndPermissionsResponseDto : IResponse
{
    public string RoleId { get; set; } = null!;
    public string RoleName { get; set; } = null!;
    public List<GetB2BUserPermissionDto>? Permissions { get; set; }
}

public class GetB2BUserPermissionDto {
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public bool Selected { get; set; }
    public bool Disabled { get; set; }
}
