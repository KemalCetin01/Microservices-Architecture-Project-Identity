using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Application.Handlers.NumberOfEmployees.Queries;

public class GetNumberOfEmployeesQuery : IQuery<ListResponse<LabelIntValueResponse>>
{

}

public sealed class GetNumberOfEmployeesQueryHandler : BaseQueryHandler<GetNumberOfEmployeesQuery, ListResponse<LabelIntValueResponse>>
{
    protected readonly INumberOfEmployeeService _NumberOfEmployeeService;
    protected readonly IMapper _mapper;

    public GetNumberOfEmployeesQueryHandler(INumberOfEmployeeService NumberOfEmployeeService, IMapper mapper)
    {
        _NumberOfEmployeeService = NumberOfEmployeeService;
        _mapper = mapper;
    }

    public override async Task<ListResponse<LabelIntValueResponse>> Handle(GetNumberOfEmployeesQuery request, CancellationToken cancellationToken)
    {
        var result = await _NumberOfEmployeeService.GetFKeyListAsync(cancellationToken);
        return new ListResponse<LabelIntValueResponse>(_mapper.Map<ICollection<LabelIntValueResponse>>(result));
    }

}
