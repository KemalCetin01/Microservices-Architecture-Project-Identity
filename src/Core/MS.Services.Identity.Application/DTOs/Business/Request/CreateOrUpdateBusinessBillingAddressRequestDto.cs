using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.AddressLocation.Request;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.DTOs.Business.Request;

public class CreateOrUpdateBusinessBillingAddressRequestDto
{
    public Guid? BusinessId { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }
    public string? PhoneNumber { get; set; }
    public BillingType BillingType { get; set; } = BillingType.Corporate;
    public string? IdentityNumber { get; set; }
    public int? CompanyType { get; set; }
    public string? TaxOffice { get; set; }
    public string? TaxNumber { get; set; }
    public string? Email { get; set; }
    public CreateOrUpdateAddressLocationRequestDto AddressLocation {get; set;} = null!;
   
}