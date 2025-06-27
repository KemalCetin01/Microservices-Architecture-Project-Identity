using MS.Services.Core.Base.Dtos;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Identity.Application.DTOs.AddressLocation.Response;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.DTOs.Business.Response;

public class GetBusinessBillingAddressResponseDto : BaseAuditableResponseDto, IResponse
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? CompanyName { get; set; }
    public string? PhoneNumber { get; set; }
    public BillingType BillingType { get; set; }
    public string? IdentityNumber { get; set; }
    public int? CompanyType { get; set; }
    public string? TaxOffice { get; set; }
    public string? TaxNumber { get; set; }
    public string? Email { get; set; }
    public GetAddressLocationResponseDto AddressLocation { get; set;} = null!;
}