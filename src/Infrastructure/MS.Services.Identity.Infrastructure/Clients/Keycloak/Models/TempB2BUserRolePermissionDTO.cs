using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.User.DTOs;

public class TempB2BUserRolePermissionDTO : IResponse
{
    public string roleId { get; set; }
    public string roleName { get; set; }
    public Guid userId { get; set; }
    public IEnumerable<B2BUserPermissionDTO>? Permissions { get; set; }
}
