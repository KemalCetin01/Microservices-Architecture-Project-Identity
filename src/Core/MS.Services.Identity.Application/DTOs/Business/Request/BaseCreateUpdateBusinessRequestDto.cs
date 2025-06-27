using MS.Services.Core.Base.Dtos;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.DTOs.Business.Request;

public abstract class BaseCreateUpdateBusinessRequestDto
{
    protected string? _name = null;
    public string Name
    {
        get => _name;
        set => _name=value.Trim();
    }

    public Guid RepresentativeId { get; set; }
    public BusinessStatusEnum BusinessStatus { get; set; }
    public int? SectorId { get; set; }
    public int? ActivityAreaId { get; set; }
    public int? NumberOfEmployeeId { get; set; }
    public int DiscountRate { get; set; }
    public string? Phone { get; set; }
    public string? PhoneCountryCode { get; set; }
    public string? FaxNumber { get; set; }
}
