using MS.Services.Core.Base.Extentions;
using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.DTOs;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Filters.CurrentAccountFilters;
using MS.Services.Identity.Persistence.Context;
using MS.Services.Identity.Persistence.Extensions;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Persistence.Repositories;

public class CurrentAccountRepository : Repository<CurrentAccount, IdentityDbContext>, ICurrentAccountRepository
{

    public CurrentAccountRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<SearchListModel<CurrentAccount>> SearchBusinessCurrentAccountsAsync(SearchQueryModel<BusinessCurrentAccountSearchFilter> searchQuery, CancellationToken cancellationToken) 
    {
        var query = Queryable();

        if(searchQuery.Filter!= null) query = query.Where(x => x.BusinessId == searchQuery.Filter.BusinessId);
        
        if (searchQuery.Filter != null) query = query.AddFilter(searchQuery.Filter);
        var currentAccountsCount = await query.CountAsync(cancellationToken);

        query = query.SortByModel(searchQuery.Sort);
        query = PaginationQuery(query, searchQuery.Pagination);
        var currentAccounts = await query.ToListAsync(cancellationToken);

        return await SearchAsync(query, searchQuery);
    }

    public async Task<long> GetCurrentAccountsCount() => await Queryable().Where(x => x.IsDeleted == false).LongCountAsync();

    public async Task<ICollection<CurrentAccount>> GetAsync(int skip, int take, CancellationToken cancellationToken  )
    {
        return await Queryable().Skip(skip).Take(take).AsNoTracking().ToListAsync(cancellationToken);

    }

    public async Task<ICollection<CurrentAccount>> GetAllAsync(string search, CancellationToken cancellationToken)
    {
        var q = Queryable();
        var searchQueryModel = new SearchQueryModel<CurrentAccountValueLabelQueryServiceFilter>();
        if (!string.IsNullOrWhiteSpace(search)) { searchQueryModel.GlobalSearch = search; }
        var result = await SearchAsync(q, searchQueryModel);

        return result.Data;

    }
    public async Task<CurrentAccount> GetById(Guid Id, CancellationToken cancellationToken  )
    {
        return await Queryable().Where(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SearchListModel<CurrentAccount>> GetGeneralCurrentAsync(SearchQueryModel<GetAllGeneralCurrentAccountsQueryFilterModel> searchQuery, CancellationToken cancellationToken  )
    {
        var query = Queryable()
           .Where(x => !x.IsDeleted)
           .Select(x => new CurrentAccount
           {
               Id = x.Id,
               Code = x.Code,
               CurrentAccountStatus = x.CurrentAccountStatus,
               CurrentAccountName = x.CurrentAccountName,
               ExchangeRate = x.ExchangeRate,
               TransactionCurrency = x.TransactionCurrency,
               SalesAndDistribution = x.SalesAndDistribution,
               SiteStatus = x.SiteStatus
           }).AsNoTrackingWithIdentityResolution();
        if (searchQuery.Filter != null)
        {
          /*  if (searchQuery.Filter.CurrentCode != null)
                query = query.Where(x => (x.Code.ToUpper()).Contains(searchQuery.Filter.CurrentCode.ToUpper()));
            if (searchQuery.Filter.currentAccountName != null)
                query = query.Where(x => (x.CurrentAccountName.ToUpper().Contains(searchQuery.Filter.currentAccountName.ToUpper())));
              if (searchQuery.Filter.TransactionCurrency != null)
                query = query.Where(x => (x.TransactionCurrency.ToUpper()).Contains(searchQuery.Filter.TransactionCurrency.ToUpper()));
            if (searchQuery.Filter.ExchangeRate != null)
                query = query.Where(x => (x.TransactionCurrency.ToUpper()).Contains(searchQuery.Filter.ExchangeRate.ToUpper()));
            if (searchQuery.Filter.CurrentAccountStatus != null)
                query = query.Where(x => x.CurrentAccountStatus==searchQuery.Filter.CurrentAccountStatus);
            if (searchQuery.Filter.SiteStatus != null)
                query = query.Where(x => x.SiteStatus == searchQuery.Filter.SiteStatus);
            if (searchQuery.Filter.SalesAndDistribution != null)
                query = query.Where(x => x.SalesAndDistribution == searchQuery.Filter.SalesAndDistribution);
                */
        }

        return await SearchAsync(query, searchQuery, cancellationToken);

    }

    protected override IQueryable<CurrentAccount> SearchOrderQuery(IQueryable<CurrentAccount> query, SortModel? sortModel)
    {
        if (sortModel == null) return query;
        return sortModel.Field switch
        {
            _ when sortModel.Field.Equals(CurrentAccountsConstants.currentAccountName, StringComparison.InvariantCultureIgnoreCase) => query.OrderByDirection(x => (x.FirstName.ToLower() + " " +x.LastName.ToLower()), sortModel.Direction.ToLower()),
            _ when sortModel.Field.Equals(nameof(CurrentAccount.Code), StringComparison.InvariantCultureIgnoreCase) => query.OrderByDirection(x => x.Code, sortModel.Direction),
            _ when sortModel.Field.Equals(nameof(CurrentAccount.CurrentAccountStatus), StringComparison.InvariantCultureIgnoreCase) => query.OrderByDirection(x => x.CurrentAccountStatus, sortModel.Direction),
            _ when sortModel.Field.Equals(nameof(CurrentAccount.SiteStatus), StringComparison.InvariantCultureIgnoreCase) => query.OrderByDirection(x => x.SiteStatus, sortModel.Direction),
            _ when sortModel.Field.Equals(nameof(CurrentAccount.CurrentAccountName), StringComparison.InvariantCultureIgnoreCase) => query.OrderByDirection(x => x.CurrentAccountName, sortModel.Direction),
            _ when sortModel.Field.Equals(nameof(CurrentAccount.TransactionCurrency), StringComparison.InvariantCultureIgnoreCase) => query.OrderByDirection(x => x.TransactionCurrency, sortModel.Direction),
            _ when sortModel.Field.Equals(nameof(CurrentAccount.ExchangeRate), StringComparison.InvariantCultureIgnoreCase) => query.OrderByDirection(x => x.ExchangeRate, sortModel.Direction),
            _ when sortModel.Field.Equals(nameof(CurrentAccount.SalesAndDistribution), StringComparison.InvariantCultureIgnoreCase) => query.OrderByDirection(x => x.SalesAndDistribution, sortModel.Direction),
            _ => query
        };
    }

    public async Task<CurrentAccount?> GetDetailByErpRefIdAsync(string erpRefId, CancellationToken cancellationToken = default)
    {
        return await Queryable()
            .Where(x => x.ErpRefId!.ToLower() == erpRefId.ToLower())
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

}