using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;

namespace MS.Services.Identity.Application.Handlers.EmployeeManagers.Commands;
public class AssignUserEmployeeManagerCommand : ICommand<DataResponse<bool>>
{
    public Guid EmployeeId { get; set; }
    public List<Guid> ManagerId { get; set; }
}

public sealed class AssignEmployeeManagerCommandHandler : BaseCommandHandler<AssignUserEmployeeManagerCommand, DataResponse<bool>>
{
    private readonly IUserEmployeeManagerService _employeeManagerService;

    public AssignEmployeeManagerCommandHandler(IUserEmployeeManagerService employeeManagerService)
    {
        _employeeManagerService = employeeManagerService;
    }

    public override async Task<DataResponse<bool>> Handle(AssignUserEmployeeManagerCommand request, CancellationToken cancellationToken)
    {
        var result = await _employeeManagerService.AssignEmployeeManager(request, cancellationToken);
        return new DataResponse<bool> { Data = result };

    }
}