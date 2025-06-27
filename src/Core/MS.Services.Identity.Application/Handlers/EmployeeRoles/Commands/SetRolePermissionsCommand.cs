using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Identity.Request;
using MS.Services.Identity.Application.Helpers;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Application.Handlers.EmployeeRoles.Commands;
public class SetRolePermissionsCommand : ICommand<DataResponse<bool>>
{
    public Guid id { get; set; }
    public List<string> PermissionIds { get; set; }

}
public sealed class SetPermissionsCommandHandler : BaseCommandHandler<SetRolePermissionsCommand, DataResponse<bool>>
{
    private readonly IEmployeeRoleService _employeeRoleService;
    private readonly IIdentityEmployeeService _identityEmployeeService;

    public SetPermissionsCommandHandler(IEmployeeRoleService employeeRoleService,
        IOptions<KeycloakOptions> options,
        IIdentityEmployeeService identityEmployeeService)
    {
        _employeeRoleService = employeeRoleService;
        _identityEmployeeService = identityEmployeeService;
    }


    public override async Task<DataResponse<bool>> Handle(SetRolePermissionsCommand request, CancellationToken cancellationToken)
    {
        var employeeRoleDetail = await _employeeRoleService.GetByIdAsync(request.id, cancellationToken);
        if (employeeRoleDetail == null)
            throw new ApiException(EmployeeRoleConstants.EmployeeRoleNotFound);

        UpdateIdentityRolePermissionsRequestDto updateRoleRequest = new UpdateIdentityRolePermissionsRequestDto
        {
            RoleName = employeeRoleDetail.Name!,
            PermissionIds = request.PermissionIds
        };
        var result = await _identityEmployeeService.UpdateRolePermissions(updateRoleRequest, cancellationToken);
        return new DataResponse<bool> { Data = result };

    }
}


