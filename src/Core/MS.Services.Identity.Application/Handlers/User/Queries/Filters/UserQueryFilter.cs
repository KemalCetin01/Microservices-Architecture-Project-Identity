using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.User.Queries.Filters
{
    public class UserQueryFilter : IFilter
    {
        public Guid Id { get; init; }
        public string? FullName { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string CurrentId { get; init; }
        public string? currentAccountName { get; set; }
        public string? UserType { get; set; }
        public SiteStatusEnum? SiteStatus { get; set; }
        public UserProfileEnum? UserProfile { get; set; }
        public Guid[]? RepresentativeIds { get; set; }
        public int[]? CountryIds { get; set; }
        public int[]? CityIds { get; set; }
        public int[]? TownIds { get; set; }
        public string? FirstRangeCreatedDate { get; set; }
        public string? LastRangeCreatedDate { get; set; }
        public string? BusinessName { get; set; }
        public Guid? BusinessId { get; init; }
        public bool? BusinessStatus { get; set; }
        public ReviewStatusEnum BusinessReviewStatus { get; init; }
    }
}
