using MS.Services.Core.Base.Dtos;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.DTOs.Business.Response;

public class GetBusinessResponseDto : BaseAuditableResponseDto, IResponse
{
    public Guid Id { get; set; }
    public Guid? IdentityRefId { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public BusinessTypeEnum? BusinessType { get; set;}
    public GetBusinessRepresentativeResponseDto? Representative { get; set; }
    public BusinessStatusEnum? BusinessStatus { get; set; }
    public ReviewStatusEnum? ReviewStatus { get; set; }
    public Guid? ReviewApprovedBy { get; set; } //TODO will be redesigned
    public Guid? ReviewRejectedBy { get; set; }
    public DateTime? ReviewApprovedDate { get; set; }
    public DateTime? ReviewRejectedDate { get; set; }
    public string? Phone { get; set; }
    public string? PhoneCountryCode { get; set; }
    public string? FaxNumber { get; set; }
    public int? SectorId { get; set; }
    public int? ActivityAreaId { get; set; }
    public int? NumberOfEmployeeId { get; set; }
    public int? DiscountRate { get; set; }
    public GetBusinessBillingAddressResponseDto? BillingAddress { get; set; }
}
