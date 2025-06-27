using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;

namespace MS.Services.Identity.Application.Handlers.CurrentAccounts.Commands;

public class DeleteCurrentAccountCommand : ICommand
{
    public Guid Id { get; set; }

}
public sealed class DeleteCurrentAccountCommandHandler : BaseCommandHandler<DeleteCurrentAccountCommand>
{
    private readonly ICurrentAccountService _currentAccountService;
    public DeleteCurrentAccountCommandHandler(ICurrentAccountService currentAccountService)
    {
        _currentAccountService = currentAccountService;
    }
    public override async Task Handle(DeleteCurrentAccountCommand request, CancellationToken cancellationToken)
    {
        await _currentAccountService.Remove(request.Id, cancellationToken);
    }
}
