using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.UserEmployees.DTOs;

namespace MS.Services.Identity.Application.Handlers.UserEmployees.Commands;
public class UpdateUserEmployeeCommand : ICommand<UserEmployeeDTO>
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }
    public string FirstName { get; set; } = null!;
    public string password { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}
public sealed class UpdateEmployeeCommandHandler : BaseCommandHandler<UpdateUserEmployeeCommand, UserEmployeeDTO>
{
    private readonly IUserEmployeeService _employeeService;

    public UpdateEmployeeCommandHandler(IUserEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public override async Task<UserEmployeeDTO> Handle(UpdateUserEmployeeCommand request, CancellationToken cancellationToken)
    {
        return await _employeeService.UpdateAsync(request, cancellationToken);

    }
}