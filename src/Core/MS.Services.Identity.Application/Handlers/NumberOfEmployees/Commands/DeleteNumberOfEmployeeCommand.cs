using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Application.Handlers.NumberOfEmployees.Commands;

public class DeleteNumberOfEmployeeCommand : ICommand
{
    public int Id { get; set; }
}
public sealed class DeleteNumberOfEmployeeCommandHandler : BaseCommandHandler<DeleteNumberOfEmployeeCommand>
{
    private readonly INumberOfEmployeeService _NumberOfEmployeeService;

    public DeleteNumberOfEmployeeCommandHandler(INumberOfEmployeeService NumberOfEmployeeService)
    {
        _NumberOfEmployeeService = NumberOfEmployeeService;
    }


    public override async Task Handle(DeleteNumberOfEmployeeCommand request, CancellationToken cancellationToken)
    {
        await _NumberOfEmployeeService.DeleteAsync(request.Id, cancellationToken);
    }
}
