using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.DTOs;

namespace MS.Services.Identity.Application.Handlers.EmployeeRoles.Commands;
public class CreateEmployeeRoleCommand : ICommand<EmployeeRoleDTO>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int DiscountRate { get; set; }
}
public sealed class CreateEmployeeRoleCommandHandler : BaseCommandHandler<CreateEmployeeRoleCommand, EmployeeRoleDTO>
{
    private readonly IEmployeeRoleService _employeeRoleService;

    public CreateEmployeeRoleCommandHandler(IEmployeeRoleService employeeRoleService)
    {
        _employeeRoleService = employeeRoleService;
    }


    public override async Task<EmployeeRoleDTO> Handle(CreateEmployeeRoleCommand request, CancellationToken cancellationToken)
    {
        return await _employeeRoleService.AddAsync(request, cancellationToken);

    }
}

