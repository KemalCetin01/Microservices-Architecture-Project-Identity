using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.EmployeeManagers.DTOs;

namespace MS.Services.Identity.Application.Handlers.EmployeeManagers.Queries;
public class GetEmployeeManagersQuery : IQuery<ListResponse<UserEmployeeManagersDTO>>
{
    public Guid userId { get; init; }
}

public sealed class GetEmployeeManagersQueryHandler : BaseQueryHandler<GetEmployeeManagersQuery, ListResponse<UserEmployeeManagersDTO>>
{
    protected readonly IUserEmployeeManagerService _employeeManagerService;
    protected readonly IMapper _mapper;

    public GetEmployeeManagersQueryHandler(IUserEmployeeManagerService employeeManagerService, IMapper mapper)
    {
        _employeeManagerService = employeeManagerService;
        _mapper = mapper;
    }

    public override async Task<ListResponse<UserEmployeeManagersDTO>> Handle(GetEmployeeManagersQuery request, CancellationToken cancellationToken  )
    {
        var result = await _employeeManagerService.GetEmployeeManagersAsync(request.userId, cancellationToken);
        return new ListResponse<UserEmployeeManagersDTO>(_mapper.Map<ICollection<UserEmployeeManagersDTO>>(result));
    }
}