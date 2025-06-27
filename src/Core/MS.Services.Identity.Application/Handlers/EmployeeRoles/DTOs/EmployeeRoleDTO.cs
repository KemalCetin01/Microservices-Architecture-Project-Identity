using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.EmployeeRoles.DTOs;
public class EmployeeRoleDTO : IResponse
{
    public Guid? Id { get; set; }
    public string? Name { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public int? DiscountRate { get; set; }
}
