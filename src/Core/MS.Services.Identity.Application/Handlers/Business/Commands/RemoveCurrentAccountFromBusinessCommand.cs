using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;

namespace MS.Services.Identity.Application.Handlers.Business.Commands;

public class RemoveCurrentAccountFromBusinessCommand : ICommand
{
    public Guid BusinessId { get; set; }
    public Guid CurrentAccountId { get; set; }
}
public sealed class DeleteCurrentAccountBusinessCommandHandler : BaseCommandHandler<RemoveCurrentAccountFromBusinessCommand>
{
    private readonly IBusinessCurrentAccountService _businessCurrentAccountService;

    public DeleteCurrentAccountBusinessCommandHandler(IBusinessCurrentAccountService businessCurrentAccountService)
    {
        _businessCurrentAccountService = businessCurrentAccountService;
    }


    public override async Task Handle(RemoveCurrentAccountFromBusinessCommand request, CancellationToken cancellationToken)
    {
        await _businessCurrentAccountService.RemoveCurrentAccountFromBusinessAsync(request.BusinessId, request.CurrentAccountId, cancellationToken);
    }
}
