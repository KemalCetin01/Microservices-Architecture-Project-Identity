using MS.Services.Core.Base.Dtos;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.CurrentAccounts.DTOs
{
    public class CurrentAccountDTO : IResponse
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? currentAccountName { get; set; }
        public string? IdentityNumber { get; set; }
        public AvailabilityEnum? CurrentAccountStatus { get; set; } // 0 kullanımda - 1 kullanım Dışı  
        public CurrentAccountTypeEnum? CurrentAccountType { get; set; } //1 alıcı - 2 satıcı - 3 alıcı satıcı
        public SiteStatusEnum? SiteStatus { get; set; } //1 aktif 0 pasif
        public AccessStatusEnum SalesAndDistribution { get; set; } //1 açık 0 kapalı
        public DateTime? CurrentCreateLogoDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string? TransactionCurrency { get; set; } //TODO:? İşlem Dövizi
        public string? ExchangeRate { get; set; } // İşlem Döviz Kuru
        public bool IsForeign{ get; set; } //Yurtdışı cari mi
        public string? CompanyType { get; set; } // Firma Tipi ŞAHIS ŞTİ. - LTD. ŞTİ. - BİREYSEL - A.Ş.
        public string? Maturity { get; set; } // Vade ?
        public bool IsSaleExchangeRate { get; set; }
    }
}
