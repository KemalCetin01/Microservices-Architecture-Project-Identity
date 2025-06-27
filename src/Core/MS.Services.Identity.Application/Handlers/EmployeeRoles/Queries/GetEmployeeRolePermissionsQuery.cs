using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.DTOs;
using MS.Services.Identity.Application.Helpers;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Application.Handlers.EmployeeRoles.Queries;
public class GetEmployeeRolePermissionsQuery : IQuery<ListResponse<EmployeeRolePermissionsDTO>>
{
    public Guid Id { get; set; }
}
public sealed class GetEmployeeRolePermissionsQueryHandler : BaseQueryHandler<GetEmployeeRolePermissionsQuery, ListResponse<EmployeeRolePermissionsDTO>>
{
    protected readonly IEmployeeRoleService _employeeRoleService;
    protected readonly IIdentityEmployeeService _identityEmployeeService;
    private readonly IMapper _mapper;
    private readonly KeycloakOptions _keycloakOptions;

    public GetEmployeeRolePermissionsQueryHandler(IEmployeeRoleService employeeRoleService,
        IIdentityEmployeeService identityEmployeeService,
        IMapper mapper,
        IOptions<KeycloakOptions> options)
    {
        _employeeRoleService = employeeRoleService;
        _identityEmployeeService = identityEmployeeService;
        _mapper = mapper;
        _keycloakOptions = options.Value;
    }

    public override async Task<ListResponse<EmployeeRolePermissionsDTO>> Handle(GetEmployeeRolePermissionsQuery request, CancellationToken cancellationToken  )
    {
        var employeeRoleDetail = await _employeeRoleService.GetByIdAsync(request.Id, cancellationToken);
        if (employeeRoleDetail == null)
            throw new ResourceNotFoundException(EmployeeRoleConstants.EmployeeRoleNotFound);

        var result = await _identityEmployeeService.GetRolePermissions(employeeRoleDetail.Name!, cancellationToken);

        return new ListResponse<EmployeeRolePermissionsDTO>(_mapper.Map<ICollection<EmployeeRolePermissionsDTO>>(result));
    }
}