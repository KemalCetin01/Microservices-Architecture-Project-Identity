using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.EmployeeManagers.DTOs;
public class UserEmployeeManagersDTO : IResponse
{
    public Guid Id { get; init; }
    public Guid ManagerId { get; init; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;

}
