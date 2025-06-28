using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Filters.UserB2CFilters
{
    public class UserB2CQueryServiceFilter : IFilterModel
    {
        public Guid Id { get; init; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? UserType { get; set; }
        public SiteStatusEnum? SiteStatus { get; set; }
        public UserProfileEnum? UserProfile { get; set; }
        public Guid[]? RepresentativeIds { get; set; }
        public int[]? CountryIds { get; set; }
        public int[]? CityIds { get; set; }
        public int[]? TownIds { get; set; }
        public string? FirstRangeCreatedDate { get; set; }
        public string? LastRangeCreatedDate { get; set; }
    }
}
