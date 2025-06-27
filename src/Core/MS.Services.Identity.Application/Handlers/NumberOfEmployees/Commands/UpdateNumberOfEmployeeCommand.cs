using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.Commands;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.DTOs;
using MS.Services.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Application.Handlers.NumberOfEmployees.Commands;

public class UpdateNumberOfEmployeeCommand : ICommand<NumberOfEmployeeDTO>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public StatusEnum Status { get; set; }
}
public sealed class UpdateNumberOfEmployeeCommandHandler : BaseCommandHandler<UpdateNumberOfEmployeeCommand, NumberOfEmployeeDTO>
{
    private readonly INumberOfEmployeeService _NumberOfEmployeeService;

    public UpdateNumberOfEmployeeCommandHandler(INumberOfEmployeeService NumberOfEmployeeService)
    {
        _NumberOfEmployeeService = NumberOfEmployeeService;
    }

    public override async Task<NumberOfEmployeeDTO> Handle(UpdateNumberOfEmployeeCommand request, CancellationToken cancellationToken)
    {
        return await _NumberOfEmployeeService.UpdateAsync(request, cancellationToken);
    }
}
