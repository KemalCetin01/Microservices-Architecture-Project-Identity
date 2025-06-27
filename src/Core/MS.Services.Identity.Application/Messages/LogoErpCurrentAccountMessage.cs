//using MS.Services.Core.Messaging.Interface;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Messages;

public class LogoErpCurrentAccountMessage//:IMessage
{
    public int LogicalRef { get; set; }
    public string? Code { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CurrentAccountName { get; set; }
    public string? IdentityNumber { get; set; }
    public string? TaxNo { get; set; }
    public DateTime? CurrentCreateLogoDate { get; set; }
    public AvailabilityEnum CurrentAccountStatus { get; set; }
    public CurrentAccountTypeEnum? CurrentAccountType { get; set; }
    public SiteStatusEnum SiteStatus { get; set; }
    public AccessStatusEnum SalesAndDistribution { get; set; }
    public bool IsForeign { get; set; } //Yurtdışı cari mi

    public string TransactionCurrency { get; set; }//TODO:? İşlem Dövizi
    public string ExchangeRate { get; set; } // İşlem Döviz Kuru
    public string? CompanyType { get; set; } // Firma Tipi
    public string Maturity { get; set; } // Vade
}
