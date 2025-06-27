using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Helpers;
using System.Net;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Application.Handlers.Business.Commands;

public sealed class DeleteBusinessCommand : ICommand
{
    public Guid Id { get; set; }
}
public sealed class DeleteBusinessCommandHandler : BaseCommandHandler<DeleteBusinessCommand>
{
    private readonly IBusinessService _businessService;
    public DeleteBusinessCommandHandler(IBusinessService businessService)
    {
        _businessService = businessService;
    }
    public override async Task Handle(DeleteBusinessCommand request, CancellationToken cancellationToken)
    {
        await _businessService.DeleteAsync(request.Id, cancellationToken);
    }
}