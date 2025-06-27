using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.DTOs;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.CurrentAccounts.Commands
{
    public class UpdateCurrentAccountCommand : ICommand<CurrentAccountDTO>
    {
        public Guid Id { get; set; }
        public SiteStatusEnum SiteStatus { get; set; }
        public AccessStatusEnum SalesAndDistribution { get; set; }
    }

    public sealed class CurrentAccoundCommandHandler : BaseCommandHandler<UpdateCurrentAccountCommand, CurrentAccountDTO>
    {
        private readonly ICurrentAccountService _currentAccountService;
        public CurrentAccoundCommandHandler(ICurrentAccountService currentAccountService)
        {
            _currentAccountService = currentAccountService;
        }
        public override async Task<CurrentAccountDTO> Handle(UpdateCurrentAccountCommand request, CancellationToken cancellationToken)
        {
            return await _currentAccountService.Update(request, cancellationToken);
        }
    }
}
