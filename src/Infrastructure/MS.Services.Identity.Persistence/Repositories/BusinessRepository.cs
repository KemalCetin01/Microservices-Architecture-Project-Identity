using MS.Services.Core.Base.Extentions;
using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Persistence.Context;
using MS.Services.Identity.Persistence.Extensions;

namespace MS.Services.Identity.Persistence.Repositories;

public class BusinessRepository : Repository<Business, IdentityDbContext>, IBusinessRepository
{
    public BusinessRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Domain.Entities.Business?> GetById(Guid Id, CancellationToken cancellationToken)
    {
        return await Queryable()
                    .AsNoTracking()
                    .Include(x => x.Representative)
                        .ThenInclude(x => x.User)
                    .Include(x => x.BillingAddress!)
                        .ThenInclude(x => x.AddressLocation)
                    .Where(x => x.Id == Id)
                    .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<Domain.Entities.Business>> SearchFKeyBusinessesAsync(string search, CancellationToken cancellationToken)
    {
        var q = Queryable();
        var searchQueryModel = new SearchQueryModel<BusinessFKeySearchFilter>();
        if (!string.IsNullOrWhiteSpace(search)) { searchQueryModel.GlobalSearch = search; }
        var result = await SearchAsync(q, searchQueryModel);
        return result.Data;
    }

    public async Task<SearchListModel<Domain.Entities.Business>> SearchAsync(SearchQueryModel<BusinessSearchFilter> searchQuery, CancellationToken cancellationToken)
    {
        var query = Queryable()
            .Include(x => x.Representative)
                .ThenInclude(x => x.User)
            .Include(x => x.Notes!)
                .ThenInclude(x => x.Note)
            .Include(x => x.BillingAddress)
                .ThenInclude(x => x.AddressLocation)
            .AsNoTrackingWithIdentityResolution();

        if (searchQuery.Filter != null) query = query.AddFilter(searchQuery.Filter);

        return await SearchAsync(query, searchQuery);
    }
    
    protected override IQueryable<Domain.Entities.Business> GlobalSearchQuery(IQueryable<Domain.Entities.Business> query, string? searchQuery)
    {
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var globalSearchUpper = searchQuery.ToUpper();
            query = query.Where(x => x.Name.ToUpper().Contains(globalSearchUpper)
                                || x.Code.ToUpper().Contains(globalSearchUpper));
        }

        return query;
    }
    protected override IQueryable<Domain.Entities.Business> SearchOrderQuery(IQueryable<Domain.Entities.Business> query, SortModel? sortModel)
    {
        return query.SortByModel(sortModel);
    }

    public Task<bool> HasExistsBusinessName(string businessName, Guid? Id, CancellationToken cancellationToken)
    {
        return Queryable().AnyAsync(x => x.Name == businessName && x.Id != Id && !x.IsDeleted, cancellationToken);
    }
}
