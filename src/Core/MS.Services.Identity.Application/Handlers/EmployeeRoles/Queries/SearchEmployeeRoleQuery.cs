using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.DTOs;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Handlers.EmployeeRoles.Queries;
public class SearchEmployeeRolesQuery : SearchQuery<SearchEmployeeRolesQueryFilter, PagedResponse<EmployeeRoleDTO>>
{
}
public sealed class SearchEmployeeRolesQueryHandler : BaseQueryHandler<SearchEmployeeRolesQuery, PagedResponse<EmployeeRoleDTO>>
{
    private readonly IMapper _mapper;
    private readonly IEmployeeRoleService _employeeRoleService;
    public SearchEmployeeRolesQueryHandler(IEmployeeRoleService employeeRoleService, IMapper mapper)
    {
        _employeeRoleService = employeeRoleService;
        _mapper = mapper;
    }
    public override async Task<PagedResponse<EmployeeRoleDTO>> Handle(SearchEmployeeRolesQuery request, CancellationToken cancellationToken  )
    {
        var searchResult = _mapper.Map<SearchQueryModel<SearchUserEmployeeRolesQueryFilterModel>>(request);

        var result = await _employeeRoleService.SearchAsync(searchResult, cancellationToken);

        return result;
    }
}
