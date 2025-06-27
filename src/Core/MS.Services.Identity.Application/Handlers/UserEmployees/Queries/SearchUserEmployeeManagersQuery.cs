using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Dtos.Response;

namespace MS.Services.Identity.Application.Handlers.UserEmployees.Queries;
public sealed class SearchUserEmployeeManagersQuery : IQuery<ListResponse<LabelValueResponse>>
{
    public string Search { get; set; }
}

public sealed class GetEmployeesQueryHandler : BaseQueryHandler<SearchUserEmployeeManagersQuery, ListResponse<LabelValueResponse>>
{
    protected readonly IUserEmployeeService _employeeService;
    protected readonly IMapper _mapper;

    public GetEmployeesQueryHandler(IUserEmployeeService employeeService, IMapper mapper)
    {
        _employeeService = employeeService;
        _mapper = mapper;
    }

    public override async Task<ListResponse<LabelValueResponse>> Handle(SearchUserEmployeeManagersQuery request, CancellationToken cancellationToken  )
    {

        var result = await _employeeService.GetEmployees(request.Search, cancellationToken);
        return new ListResponse<LabelValueResponse>(_mapper.Map<ICollection<LabelValueResponse>>(result));
    }

}