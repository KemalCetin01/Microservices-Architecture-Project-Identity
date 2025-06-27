using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.DTOs;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Application.Handlers.NumberOfEmployees.Queries;

public class GetNumberOfEmployeeDetailsQuery : IQuery<NumberOfEmployeeDTO>
{
    public int Id { get; set; }
}
public sealed class GetNumberOfEmployeeDetailQueryHandler : BaseQueryHandler<GetNumberOfEmployeeDetailsQuery, NumberOfEmployeeDTO>
{
    protected readonly INumberOfEmployeeService _NumberOfEmployeeService;

    public GetNumberOfEmployeeDetailQueryHandler(INumberOfEmployeeService NumberOfEmployeeService)
    {
        _NumberOfEmployeeService = NumberOfEmployeeService;
    }

    public override async Task<NumberOfEmployeeDTO> Handle(GetNumberOfEmployeeDetailsQuery request, CancellationToken cancellationToken)
    {
        return await _NumberOfEmployeeService.GetByIdAsync(request.Id, cancellationToken);
    }
}
