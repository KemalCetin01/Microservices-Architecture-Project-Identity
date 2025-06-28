using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.EntityFilters;

public class GetAllGeneralCurrentAccountsQueryFilterModel : IFilterModel
{
    public Guid? CurrentId { get; set; }
    public string? currentAccountName { get; set; }
    public string? CurrentCode { get; set; }
    public AvailabilityEnum? CurrentAccountStatus { get; set; }
    public SiteStatusEnum? SiteStatus { get; set; }
    public string? TransactionCurrency { get; set; } //TODO:? İşlem Dövizi
    public string? ExchangeRate { get; set; } // İşlem Döviz Kuru
    public AccessStatusEnum SalesAndDistribution { get; set; }
}
