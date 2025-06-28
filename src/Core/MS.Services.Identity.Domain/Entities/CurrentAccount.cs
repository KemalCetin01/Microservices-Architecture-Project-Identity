using MS.Services.Core.Data.Data.Attributes;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities
{
    public class CurrentAccount : BaseSoftDeleteEntity
    {
        public string ErpRefId { get; set; } = null!;
        
        [QuerySearch]
        public string Code { get; set; } = null!;
        [QuerySearch]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string CurrentAccountName { get; set; } = null!;
        public string? IdentityNumber { get; set; }
        public string? TaxNo { get; set; } 
        public DateTime? CurrentCreateLogoDate { get; set; }
        public CurrentAccountTypeEnum? CurrentAccountType { get; set; }
        public AvailabilityEnum? CurrentAccountStatus { get; set; }
        public SiteStatusEnum? SiteStatus{ get; set; }
        public AccessStatusEnum? SalesAndDistribution { get; set; }
        public bool IsForeign{ get; set; } //Yurtdışı cari mi
        public string TransactionCurrency { get; set; }//TODO:? İşlem Dövizi
        public string ExchangeRate{ get; set; } // İşlem Döviz Kuru
        public string? CompanyType { get; set; } // Firma Tipi
        public string? Maturity { get; set; } // Vade
        public bool IsSaleExchangeRate { get; set; }
        public Guid? BusinessId { get; set; }

    }
}
