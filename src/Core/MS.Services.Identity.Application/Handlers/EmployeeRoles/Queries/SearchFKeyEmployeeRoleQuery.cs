using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Dtos.Response;

namespace MS.Services.Identity.Application.Handlers.EmployeeRoles.Queries;
public class SearchFKeyEmployeeRoleQuery : IQuery<ListResponse<LabelValueResponse>>
{
    public string Search { get; set; }

}
public sealed class SearchFKeyEmployeeRoleQueryHandler : BaseQueryHandler<SearchFKeyEmployeeRoleQuery, ListResponse<LabelValueResponse>>
{
    protected readonly IEmployeeRoleService _employeeRoleService;
    protected readonly IMapper _mapper;

    public SearchFKeyEmployeeRoleQueryHandler(IEmployeeRoleService employeeRoleService, IMapper mapper)
    {
        _employeeRoleService = employeeRoleService;
        _mapper = mapper;
    }

    public override async Task<ListResponse<LabelValueResponse>> Handle(SearchFKeyEmployeeRoleQuery request, CancellationToken cancellationToken  )
    {

        var result = await _employeeRoleService.SearchFKeyAsync(request.Search, cancellationToken);
        return new ListResponse<LabelValueResponse>(_mapper.Map<ICollection<LabelValueResponse>>(result));
    }

}