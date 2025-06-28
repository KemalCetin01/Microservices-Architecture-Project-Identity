using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.EntityFilters;
public class BusinessSearchFilter : IFilterModel
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? CountryCode { get; set; }
    public BusinessTypeEnum? BusinessType { get; set; }
    public List<Guid>? RepresentativeIds { get; set; }
    public BusinessStatusEnum? BusinessStatus { get; set; }
    public ReviewStatusEnum? ReviewStatus { get; set; }
}