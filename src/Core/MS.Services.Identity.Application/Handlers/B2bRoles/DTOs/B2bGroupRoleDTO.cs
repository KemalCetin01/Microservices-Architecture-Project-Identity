using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.B2bRoles.DTOs;

public class B2bGroupRoleDTO : IResponse
{
    public Guid? Id { get; set; }
    public string? Name { get; set; } = null!;
}
