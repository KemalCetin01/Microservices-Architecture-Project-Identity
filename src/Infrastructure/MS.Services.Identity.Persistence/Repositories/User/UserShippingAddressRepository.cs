using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Domain.Filters.AddressFilters;
using MS.Services.Identity.Persistence.Context;

namespace MS.Services.Identity.Persistence.Repositories;

public class UserShippingAddressRepository : Repository<UserShippingAddress, IdentityDbContext>, IUserShippingAddressRepository
{
    public UserShippingAddressRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<UserShippingAddress> GetById(Guid Id, CancellationToken cancellationToken  )
    {
        return await Queryable()
           .Include(x => x.AddressLocation)
           .Where(x => x.Id == Id && x.AddressLocation.IsDeleted == false).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<UserShippingAddress> GetByAddressId(Guid AddressId, CancellationToken cancellationToken)
    {
        return await Queryable()
           .Include(x => x.AddressLocation)
           .Where(x => x.AddressLocationId == AddressId).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SearchListModel<UserShippingAddress>> GetAddressByUserId(SearchQueryModel<AddressQueryServiceFilter> searchQuery, Guid? userId, CancellationToken cancellationToken)
    {

        //globaldan yazma search text ile yaz 
        var query = Queryable();
        query = query.Include(x => x.User);
        query = query.Include(x => x.AddressLocation);

        query = query.Where(x => x.UserId == userId);
        query = query.Where(x => x.AddressLocation.IsDeleted == false);
        if (searchQuery.Filter != null)
        {
            if (searchQuery.Filter.FullName != null)
                query = query.Where(x => x.DeliveryContactName.ToUpper().Contains(searchQuery.Filter.FullName.ToUpper()));
            if (searchQuery.Filter.Name != null)
                query = query.Where(x => x.Name.ToUpper().Contains(searchQuery.Filter.Name.ToUpper()));
            //if (searchQuery.Filter.Country != null)
            //    query = query.Where(x => x.Address.Country.Name.ToUpper().Contains(searchQuery.Filter.Country.ToUpper()));
            //if (searchQuery.Filter.City != null)
            //    query = query.Where(x => x.Address.City.Name.ToUpper().Contains(searchQuery.Filter.City.ToUpper()));
            //  if (searchQuery.Filter.BillingType != null)
            //result = result.Where(x => x.BillingDetail.BillingType.Contains(searchQuery.BillingType));
            if (searchQuery.Filter.ZipCode != null)
                query = query.Where(x => x.AddressLocation.ZipCode.Contains(searchQuery.Filter.ZipCode));
        }

        return await SearchAsync(query, searchQuery, cancellationToken);

    }

    protected override IQueryable<UserShippingAddress> GlobalSearchQuery(IQueryable<UserShippingAddress> query, string? searchQuery)
    {
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var globalSearch = searchQuery.ToUpper();
            query = query.Where(x => (x.DeliveryContactName != null ? x.DeliveryContactName.ToUpper() : "").Contains(globalSearch)
                             || x.Name!.ToUpper().Contains(globalSearch)
                             || x.AddressLocation.ZipCode!.ToUpper().Contains(globalSearch));
        }

        return query;
    }
}
