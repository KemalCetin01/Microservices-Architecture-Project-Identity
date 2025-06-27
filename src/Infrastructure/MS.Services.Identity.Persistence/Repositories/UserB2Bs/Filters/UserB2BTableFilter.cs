using MS.Services.Identity.Domain.Filters.UserB2BFilters;

namespace MS.Services.Identity.Persistence.Repositories;

public class UserB2BTableFilter
{
    public static IQueryable<UserB2B> Filter(IQueryable<UserB2B> query, UserB2BQueryServiceFilter userQueryFilter)
    {
        if (userQueryFilter.Id != Guid.Empty) { query = query.Where(x => x.UserId == userQueryFilter.Id); }
        if (!string.IsNullOrWhiteSpace(userQueryFilter.FullName)) { query = query.Where(x => (x.User.FirstName + " " + x.User.LastName).ToUpper().Contains(userQueryFilter.FullName.ToUpper())); }
        if (!string.IsNullOrWhiteSpace(userQueryFilter.Email)) { query = query.Where(x => x.User.Email.Contains(userQueryFilter.Email)); }

        if (!string.IsNullOrWhiteSpace(userQueryFilter.CurrentId))
        {
            //query = query.Where(x => x.BusinessUsers.FirstOrDefault().Business.BusinessCurrentAccounts.FirstOrDefault().CurrentAccount.Code.ToUpper().Contains(userQueryFilter.CurrentId.ToUpper()));
        }
        //if (userQueryFilter.CurrentAccountName != null) { query = query.Where(x => x.BusinessUsers.FirstOrDefault().Business.CurrentAccountBusinesses.FirstOrDefault().CurrentAccount.CurrentAccountName == userQueryFilter.CurrentAccountName); }
        if (userQueryFilter.BusinessName != null) { query = query.Where(x => x.BusinessUsers.FirstOrDefault().Business.Name.ToUpper().Contains(userQueryFilter.BusinessName.ToUpper())); }
        if (userQueryFilter.BusinessId != null) { query = query.Where(x => x.BusinessUsers.FirstOrDefault().BusinessId == userQueryFilter.BusinessId); }
        //if (userQueryFilter.CurrentAccountStatus != null) { query = query.Where(x => x.BusinessUsers.FirstOrDefault().Business.BusinessCurrentAccounts.FirstOrDefault().CurrentAccount.CurrentAccountStatus == userQueryFilter.CurrentAccountStatus); }
       // if (userQueryFilter.BusinessStatus != null) { query = query.Where(x => x.BusinessUsers.FirstOrDefault().Business.BusinessStatus == userQueryFilter.BusinessStatus); }
       //TODO business status'a gore filter
        if (userQueryFilter.BusinessReviewStatus!=0) { query = query.Where(x => x.BusinessUsers.FirstOrDefault().Business.ReviewStatus == userQueryFilter.BusinessReviewStatus); }

        if (userQueryFilter.SiteStatus != null) { query = query.Where(x => x.SiteStatus == userQueryFilter.SiteStatus); }
        if (userQueryFilter.RepresentativeIds != null)
        {
            if (userQueryFilter.RepresentativeIds.Length > 0)
            {
                query = query.Where(x => userQueryFilter.RepresentativeIds.ToList().Contains((Guid)x.UserEmployeeId));
            }
        }
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
