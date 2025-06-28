using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Filters.UserFilters;

public class UserQueryServiceFilter : IFilterModel
{
    public Guid Id { get; init; }
    public string? FullName { get; set; } = null!;
    public string? Email { get; set; } = null!;
    public string CurrentId { get; init; }
    public string? currentAccountName { get; set; }
    public bool? CurrentStatus { get; set; }
    public string? UserType { get; set; }
    public SiteStatusEnum? SiteStatus { get; set; }
    public UserProfileEnum? UserProfile { get; set; }
    public string? Representative { get; set; }
    public string[]? Country { get; set; }
    public string[]? City { get; set; }
    public string[]? Town { get; set; }
    public string? FirstRangeCreatedDate { get; set; }
    public string? LastRangeCreatedDate { get; set; }
}
