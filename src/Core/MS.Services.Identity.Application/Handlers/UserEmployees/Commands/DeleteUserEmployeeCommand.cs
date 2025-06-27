using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;

namespace MS.Services.Identity.Application.Handlers.UserEmployees.Commands;
public class DeleteUserEmployeeCommand : ICommand
{
    public Guid userId { get; set; }
}

public sealed class DeleteEmployeeCommandHandler : BaseCommandHandler<DeleteUserEmployeeCommand>
{
    private readonly IUserEmployeeService _employeeService;

    public DeleteEmployeeCommandHandler(IUserEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public override async Task Handle(DeleteUserEmployeeCommand request, CancellationToken cancellationToken)
    {
        await _employeeService.DeleteAsync(request.userId, cancellationToken);
    }
}