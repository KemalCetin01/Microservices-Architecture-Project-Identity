using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.DTOs.Identity.Response;
public class GetRolePermissionsResponseDto
{
    public string Application { get; set; } = null!;
    public string ContainerId { get; set; } = null!;
    public List<GetPermissionResponseDto>? Permissions { get; set; }
}

public class GetPermissionResponseDto
{
    public string Id { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public bool selected { get; set; }
}