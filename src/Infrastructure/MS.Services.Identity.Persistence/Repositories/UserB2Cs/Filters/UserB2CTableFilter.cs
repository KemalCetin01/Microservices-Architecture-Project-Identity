using MS.Services.Identity.Domain.Filters.UserB2CFilters;

namespace MS.Services.Identity.Persistence.Repositories;

public class UserB2CTableFilter
{
    public static IQueryable<UserB2C> Filter(IQueryable<UserB2C> query, UserB2CQueryServiceFilter userQueryFilter)
    {
        if (userQueryFilter.Id != Guid.Empty) { query = query.Where(x => x.UserId == userQueryFilter.Id); }
        if (!string.IsNullOrWhiteSpace(userQueryFilter.FullName)) { query = query.Where(x => (x.User.FirstName + " " + x.User.LastName).ToUpper().Contains(userQueryFilter.FullName.ToUpper())); }
        if (!string.IsNullOrWhiteSpace(userQueryFilter.Email)) { query = query.Where(x => x.User.Email.ToUpper().Contains(userQueryFilter.Email.ToUpper())); }

        if (userQueryFilter.SiteStatus != null) { query = query.Where(x => x.SiteStatus == userQueryFilter.SiteStatus); }
        //if (userQueryFilter.UserType != null) { if (userQueryFilter.UserType == "Yurtiçi") { query = query.Where(x => x.Country.Name == "Türkiye"); } else { query = query.Where(x => x.Country.Name != "Türkiye"); } }
        if (userQueryFilter.RepresentativeIds.Length > 0) { query = query.Where(x => userQueryFilter.RepresentativeIds.ToList().Contains((Guid)x.UserEmployeeId)); }
        if (userQueryFilter.CountryIds.Length > 0) { query = query.Where(x => userQueryFilter.CountryIds.ToList().Contains((int)x.CountryId)); }
        if (userQueryFilter.CityIds.Length > 0) { query = query.Where(x => userQueryFilter.CityIds.ToList().Contains((int)x.CityId)); }
        if (userQueryFilter.TownIds.Length > 0) { query = query.Where(x => userQueryFilter.TownIds.ToList().Contains((int)x.TownId)); }
        if (userQueryFilter.FirstRangeCreatedDate != null)
        {
            DateTime frcreated = Convert.ToDateTime(userQueryFilter.FirstRangeCreatedDate);
            query = query.Where(x => x.User.CreatedDate >= frcreated.ToUniversalTime());
        }
        if (userQueryFilter.LastRangeCreatedDate != null)
        {
            DateTime lrcreated = Convert.ToDateTime(userQueryFilter.LastRangeCreatedDate).AddDays(1).AddSeconds(-1);
            query = query.Where(x => x.User.CreatedDate <= lrcreated.ToUniversalTime());
        }
        return query;
    }
}
