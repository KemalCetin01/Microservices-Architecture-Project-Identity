using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Data.Data.Attributes;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.DTOs;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.CurrentAccounts.Commands;

public class CreateCurrentAccountCommand : ICommand<CurrentAccountDTO>
{
    public string? ErpRefId { get; set; }
    public string? Code { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? IdentityNumber { get; set; } //logoda string
    public DateTime? CurrentCreateLogoDate { get; set; }
    public CurrentAccountTypeEnum? CurrentAccountType { get; set; }//TOODO??
    public AvailabilityEnum? CurrentAccountStatus { get; set; }
    public SiteStatusEnum? SiteStatus { get; set; }
    public AccessStatusEnum? SalesAndDistribution { get; set; }
    public bool IsForeign { get; set; } //Yurtdışı cari mi
    public string TransactionCurrency { get; set; }//TODO:? İşlem Dövizi
    public string ExchangeRate { get; set; } // İşlem Döviz Kuru
    public string? CompanyType { get; set; } // Firma Tipi
    public string? Maturity { get; set; } // Vade
    public bool IsSaleExchangeRate { get; set; }

}
public sealed class CreateCurrentAccountCommandHandler : BaseCommandHandler<CreateCurrentAccountCommand, CurrentAccountDTO>
{
    private readonly ICurrentAccountService _currentAccountService;
    public CreateCurrentAccountCommandHandler(ICurrentAccountService currentAccountService)
    {
        _currentAccountService = currentAccountService;
    }
    public override async Task<CurrentAccountDTO> Handle(CreateCurrentAccountCommand request, CancellationToken cancellationToken)
    {
        return await _currentAccountService.AddAsync(request, cancellationToken);
    }
}