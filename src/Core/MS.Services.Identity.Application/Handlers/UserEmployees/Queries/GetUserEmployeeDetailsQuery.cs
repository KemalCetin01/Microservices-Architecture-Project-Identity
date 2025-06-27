using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.UserEmployees.DTOs;

namespace MS.Services.Identity.Application.Handlers.UserEmployees.Queries;

public class GetUserEmployeeDetailsQuery : IQuery<UserEmployeeDTO>
{
    public Guid userId { get; set; }
}
public sealed class GetEmployeeDetailsQueryHandler : BaseQueryHandler<GetUserEmployeeDetailsQuery, UserEmployeeDTO>
{
    protected readonly IUserEmployeeService _employeeService;

    public GetEmployeeDetailsQueryHandler(IUserEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    public override async Task<UserEmployeeDTO> Handle(GetUserEmployeeDetailsQuery request, CancellationToken cancellationToken  )
    {
        return await _employeeService.GetByIdAsync(request.userId, cancellationToken);

    }
}