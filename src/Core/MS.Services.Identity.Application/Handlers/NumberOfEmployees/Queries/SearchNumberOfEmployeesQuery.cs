using AutoMapper;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.DTOs;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.Queries;
using MS.Services.Identity.Domain.EntityFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Application.Handlers.NumberOfEmployees.Queries;

public class SearchNumberOfEmployeesQuery : SearchQuery<SearchNumberOfEmployeesQueryFilter, PagedResponse<NumberOfEmployeeDTO>>
{
}
public sealed class SearchNumberOfEmployeesQueryHandler : BaseQueryHandler<SearchNumberOfEmployeesQuery, PagedResponse<NumberOfEmployeeDTO>>
{
    protected readonly INumberOfEmployeeService _NumberOfEmployeeService;
    protected readonly IMapper _mapper;

    public SearchNumberOfEmployeesQueryHandler(INumberOfEmployeeService NumberOfEmployeeService, IMapper mapper)
    {
        _NumberOfEmployeeService = NumberOfEmployeeService;
        _mapper = mapper;
    }

    public override async Task<PagedResponse<NumberOfEmployeeDTO>> Handle(SearchNumberOfEmployeesQuery request, CancellationToken cancellationToken)
    {
        var searchResult = _mapper.Map<SearchQueryModel<SearchNumberOfEmployeeQueryFilterModel>>(request);

        return await _NumberOfEmployeeService.SearchAsync(searchResult, cancellationToken);

    }

}
