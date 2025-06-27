using MS.Services.Core.Base.Dtos;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.DTOs.AddressLocation.Response;

public class GetAddressLocationResponseDto : BaseAuditableResponseDto, IResponse
{
    public Guid Id { get; set; }
    public string? CountryCode { get; set; }
    public int? CityId { get; set; }
    public string? CityName { get; set; }
    public int? TownId { get; set; }
    public string? TownName { get; set; }
    public int? DistrictId { get; set; }
    public string? DistrictName { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? ZipCode { get; set; }
    public string? AddressDescription { get; set; }
}