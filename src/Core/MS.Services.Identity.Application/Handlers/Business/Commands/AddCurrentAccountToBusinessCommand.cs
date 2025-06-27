using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;

namespace MS.Services.Identity.Application.Handlers.Business.Commands;

public class AddCurrentAccountToBusinessCommand : ICommand
{
    public Guid CurrentAccountId { get; set; }
    public Guid BusinessId { get; set; }

}
public sealed class CreateCurrentAccountBusinessCommandHandler : BaseCommandHandler<AddCurrentAccountToBusinessCommand>
{
    private readonly IBusinessCurrentAccountService _businessCurrentAccountService;

    public CreateCurrentAccountBusinessCommandHandler(IBusinessCurrentAccountService businessCurrentAccountService)
    {
        _businessCurrentAccountService = businessCurrentAccountService;
    }


    public override async Task Handle(AddCurrentAccountToBusinessCommand request, CancellationToken cancellationToken)
    {
        await _businessCurrentAccountService.AddCurrentAccountToBusinessAsync(request.BusinessId, request.CurrentAccountId, cancellationToken);
    }
}
