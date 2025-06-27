using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.DTOs.Business.Response;

public class SearchBusinessResponseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Code { get; set; } = null!;
    public GetBusinessRepresentativeResponseDto? Representative { get; set; }
    public BusinessStatusEnum? BusinessStatus { get; set; }
    public ReviewStatusEnum? ReviewStatus { get; set; }
    public string? CountryCode { get; set; }
    public BusinessTypeEnum? BusinessType { get; set;}
    public int? DiscountRate { get; set; }
    public string? Note { get; set; }

}