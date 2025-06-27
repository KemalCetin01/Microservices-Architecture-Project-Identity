using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.EmployeeRoles.DTOs;
public class EmployeeRoleKeyValueDTO : IResponse
{
    public Guid Value { get; init; }
    public string Label { get; set; } = null!;
}