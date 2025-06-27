using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.UserEmployees.DTOs;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Handlers.UserEmployees.Queries;
public class SearchUserEmployeesQuery : SearchQuery<SearchUserEmployeesQueryFilter, PagedResponse<UserEmployeeDTO>>
{
}
public sealed class SearchEmployeesQueryHandler : BaseQueryHandler<SearchUserEmployeesQuery, PagedResponse<UserEmployeeDTO>>
{
    private readonly IMapper _mapper;
    private readonly IUserEmployeeService _employeeService;
    public SearchEmployeesQueryHandler(IUserEmployeeService employeeService, IMapper mapper)
    {
        _mapper = mapper;
        _employeeService = employeeService;
    }
    public override async Task<PagedResponse<UserEmployeeDTO>> Handle(SearchUserEmployeesQuery request, CancellationToken cancellationToken  )
    {
        var searchResult = _mapper.Map<SearchQueryModel<SearchUserEmployeesQueryFilterModel>>(request);

        var result = await _employeeService.SearchAsync(searchResult, cancellationToken);

        return result;
    }
}
