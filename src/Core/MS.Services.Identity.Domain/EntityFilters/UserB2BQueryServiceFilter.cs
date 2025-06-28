using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Filters.UserB2BFilters;

public class UserB2BQueryServiceFilter : IFilterModel
{
    public Guid Id { get; init; }
    public string? FullName { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string CurrentId { get; init; }
    public string? CurrentAccountName { get; set; }
    public AvailabilityEnum? CurrentAccountStatus { get; set; }
    public SiteStatusEnum? SiteStatus { get; set; }
    public Guid[]? RepresentativeIds { get; set; }
    public string? FirstRangeCreatedDate { get; set; }
    public string? LastRangeCreatedDate { get; set; }
    public string? BusinessName { get; set; }
    public Guid? BusinessId { get; init; }
    public bool? BusinessStatus { get; set; }
    public ReviewStatusEnum BusinessReviewStatus { get; init; }
}
