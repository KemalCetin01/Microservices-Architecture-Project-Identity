using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

public abstract class BaseBillingAddress : BaseAddress
{
    public BillingType BillingType { get; set; }
    public string? Email { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }
    public int? CompanyType { get; set; } //TODO can be converted to database table
    public string? TaxNumber { get; set; }
    public string? IdentityNumber { get; set; }
    public string? TaxOffice { get; set; }

}
