using MS.Services.Core.Base.Extentions;
using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Domain.Filters.UserB2BFilters;
using MS.Services.Identity.Persistence.Context;
using MS.Services.Identity.Persistence.Extensions;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Persistence.Repositories;

public class UserB2BRepository : Repository<UserB2B, IdentityDbContext>, IUserB2BRepository
{
    public UserB2BRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<SearchListModel<UserB2B>> SearchBusinessUsersAsync(SearchQueryModel<BusinessUserSearchFilter> searchQuery, CancellationToken cancellationToken) {
        
        var query = Queryable();

        query = query.Include(x => x.User).Include(x => x.Businesses);
        query = query.Where(x => x.IsDeleted == false);

        if(searchQuery.Filter!= null) query = query.Where(x => x.Businesses!.Any(x => x.Id == searchQuery.Filter.BusinessId));
        
        if (searchQuery.Filter != null) query = query.AddFilter(searchQuery.Filter);
        var b2bUsersCount = await query.CountAsync(cancellationToken);

        query = query.SortByModel(searchQuery.Sort);
        query = PaginationQuery(query, searchQuery.Pagination);
        var b2bUsers = await query.ToListAsync(cancellationToken);

        return new SearchListModel<UserB2B>(b2bUsers, searchQuery.Pagination?.CurrentPage, searchQuery.Pagination?.PageSize, b2bUsersCount);
    }

    public async Task<SearchListModel<UserB2B>> GetAll(SearchQueryModel<UserB2BQueryServiceFilter> searchQuery, CancellationToken cancellationToken)
    {
        var query = Queryable();
        query = query.Include(x => x.User);
        query = query.Include(x => x.UserEmployee).ThenInclude(x => x.User);
        //query = query.Include(x => x.User.CreatedByUser);
        query = query.Include(x => x.BusinessUsers).ThenInclude(x => x.Business);
        query = query.Where(x => x.User.IsDeleted == false);

        if (searchQuery.Filter != null) { query = UserB2BTableFilter.Filter(query, searchQuery.Filter); }
        return await SearchAsync(query, searchQuery, cancellationToken);
    }

    public async Task<UserB2B> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await Queryable()
           .Include(x => x.User)
           .Include(x => x.BusinessUsers)
           .ThenInclude(x => x.Business)
           .ThenInclude(x => x.CurrentAccounts)
           .Where(x => x.UserId == id && x.User.IsDeleted == false).FirstOrDefaultAsync(cancellationToken);
    }

    protected override IQueryable<UserB2B> SearchOrderQuery(IQueryable<UserB2B> query, SortModel? sortModel)
    {
        if (sortModel == null) return query;
        return sortModel.Field switch
        {
            _ when sortModel.Field.Equals(B2BUserConstants.fullName, StringComparison.InvariantCultureIgnoreCase) => query.OrderByDirection(x => (x.User.FirstName.ToLower() + " " + x.User.LastName.ToLower()), sortModel.Direction.ToLower()),
            _ when sortModel.Field.Equals(nameof(UserB2B.User.Email), StringComparison.InvariantCultureIgnoreCase) => query.OrderByDirection(x => x.User.Email, sortModel.Direction),
            _ when sortModel.Field.Equals(B2BUserConstants.representative, StringComparison.InvariantCultureIgnoreCase) => query.OrderByDirection(x => (x.UserEmployee.User.FirstName + " " + x.UserEmployee.User.LastName), sortModel.Direction),
            _ => query
        };
    }

    protected override IQueryable<UserB2B> GlobalSearchQuery(IQueryable<UserB2B> query, string? searchQuery)
    {
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var globalSearch = searchQuery.ToUpper();
            query = query.Where(x => (x.User.FirstName + " " + x.User.LastName).ToUpper().Contains(globalSearch)
                            || (x.UserEmployee.User.FirstName + " " + x.UserEmployee.User.LastName).ToUpper().Contains(globalSearch)
                             || x.User.Email!.ToUpper().Contains(globalSearch));
        }

        return query;
    }

    public async Task<int> GetActiveUserInRoleCountAsync(Guid userGroupRoleId, CancellationToken cancellationToken)
    => await Queryable().Where(x => !x.IsDeleted && x.UserGroupRoleId == userGroupRoleId).CountAsync(cancellationToken);

    public async Task<UserB2B?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Queryable().Include(x => x.User).Where(x => x.User.Email == email && x.User.UserTypeId == (int)UserTypeEnum.B2B && !x.IsDeleted).FirstOrDefaultAsync();
    }

    public Task<List<UserB2B>> GetUsersByBusinessKeycloakId(Guid identityGroupRefId, CancellationToken cancellationToken)
    {
        var userB2BList = Queryable().Include(x => x.User)
           .Include(x => x.BusinessUsers)
           .Where(x => x.BusinessUsers.FirstOrDefault().Business.IdentityRefId == identityGroupRefId && !x.IsDeleted)
           .ToListAsync(cancellationToken);
        return userB2BList;
    }

}
