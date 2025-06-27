using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Domain.Filters.UserB2CFilters;
using MS.Services.Identity.Persistence.Context;
using MS.Services.Identity.Persistence.Repositories;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Persistence.Repositories;

public class UserB2CRepository : Repository<UserB2C, IdentityDbContext>, IUserB2CRepository
{
    public UserB2CRepository(IdentityDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<SearchListModel<UserB2C>> GetAll(SearchQueryModel<UserB2CQueryServiceFilter> searchQuery, CancellationToken cancellationToken)
    {
        var query = Queryable();
        query = query.Include(x => x.User).ThenInclude(x => x.Notes).ThenInclude(x => x.Note);
        query = query.Include(x => x.UserEmployee).ThenInclude(x => x.User);
        //query = query.Include(x => x.User.CreatedByUser);
        query = query.Include(x => x.User.UserShippingAddresses);
        query = query.Include(x => x.User.UserBillingAddresses);
        query = query.Where(x => x.User.IsDeleted == false);

        if (searchQuery.Filter != null) { query = UserB2CTableFilter.Filter(query, searchQuery.Filter); }
        return await SearchAsync(query, searchQuery, cancellationToken);
    }

    public async Task<UserB2C> GetById(Guid id, CancellationToken cancellationToken)
    {
        return await Queryable().Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == id && x.User.IsDeleted == false, cancellationToken);
    }

    protected override IQueryable<UserB2C> SearchOrderQuery(IQueryable<UserB2C> query, SortModel? sortModel)
    {
        if (sortModel == null)
        {
            return query;
        }
        if (sortModel.Direction.ToUpper() == "DESC")
        {
            switch (sortModel.Field)
            {
                case B2CUserConstants.email:
                    query = query.OrderByDescending(x => x.User.Email);
                    break;
                case B2CUserConstants.fullName:
                    query = query.OrderByDescending(x => (x.User.FirstName + " " + x.User.LastName));
                    break;
                case B2CUserConstants.representative:
                    query = query.OrderByDescending(x => (x.UserEmployee.User.FirstName + " " + x.UserEmployee.User.LastName));
                    break;
                case B2CUserConstants.CreatedDate:
                    query = query.OrderByDescending(x => x.User.CreatedDate);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (sortModel.Field)
            {
                case B2CUserConstants.email:
                    query = query.OrderBy(x => x.User.Email);
                    break;
                case B2CUserConstants.fullName:
                    query = query.OrderBy(x => (x.User.FirstName + " " + x.User.LastName));
                    break;
                case B2CUserConstants.representative:
                    query = query.OrderBy(x => (x.UserEmployee.User.FirstName + " " + x.UserEmployee.User.LastName));
                    break;
                case B2CUserConstants.CreatedDate:
                    query = query.OrderBy(x => x.User.CreatedDate);
                    break;
                default:
                    break;
            }
        }
        return query;
    }

    protected override IQueryable<UserB2C> GlobalSearchQuery(IQueryable<UserB2C> query, string? searchQuery)
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
    public async Task<UserB2C?> GetByPhoneAsync(string phone, CancellationToken cancellationToken)
    {
        return await Queryable()
        .Include(x => x.User)
        .Where(x => (x.PhoneCountryCode + x.Phone) == phone && x.User.UserTypeId == (int)UserTypeEnum.B2C && !x.IsDeleted)
        .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<UserB2C?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await Queryable().Include(x => x.User).Where(x => x.User.Email == email && x.User.UserTypeId == (int)UserTypeEnum.B2C && !x.IsDeleted).FirstOrDefaultAsync();
    }

    // deleted kayıtlar default olarak dönmüyor.
    public async Task<bool> AnotherUserHasPhoneAsync(Guid? userId, string phoneCountryCode, string phone, CancellationToken cancellationToken)
    {
        var query = Queryable();
        if (userId != null) query = query.Where(x => x.UserId != userId);
        return await query.AnyAsync(x => x.PhoneCountryCode == phoneCountryCode && x.Phone == phone, cancellationToken);
    }

}
