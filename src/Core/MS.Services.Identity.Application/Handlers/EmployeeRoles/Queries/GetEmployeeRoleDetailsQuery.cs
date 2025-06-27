using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.DTOs;

namespace MS.Services.Identity.Application.Handlers.EmployeeRoles.Queries;
public class GetEmployeeRoleDetailsQuery : IQuery<EmployeeRoleDTO>
{
    public Guid Id { get; set; }
}
public sealed class GetEmployeeRoleDetailsQueryHandler : BaseQueryHandler<GetEmployeeRoleDetailsQuery, EmployeeRoleDTO>
{
    protected readonly IEmployeeRoleService _employeeRoleService;

    public GetEmployeeRoleDetailsQueryHandler(IEmployeeRoleService employeeRoleService)
    {
        _employeeRoleService = employeeRoleService;
    }

    public override async Task<EmployeeRoleDTO> Handle(GetEmployeeRoleDetailsQuery request, CancellationToken cancellationToken  )
    {
        return await _employeeRoleService.GetByIdAsync(request.Id, cancellationToken);

    }
}