using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.DTOs;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Filters.UserB2BFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface ICurrentAccountRepository : IRepository<CurrentAccount>
{
    Task<SearchListModel<CurrentAccount>> SearchBusinessCurrentAccountsAsync(SearchQueryModel<BusinessCurrentAccountSearchFilter> searchQuery, CancellationToken cancellationToken);
    Task<SearchListModel<CurrentAccount>> GetGeneralCurrentAsync(SearchQueryModel<GetAllGeneralCurrentAccountsQueryFilterModel> searchQuery, CancellationToken cancellationToken);
    Task<ICollection<CurrentAccount>> GetAsync(int skip, int take, CancellationToken cancellationToken  );
    Task<CurrentAccount> GetById(Guid Id, CancellationToken cancellationToken  );
    Task<long> GetCurrentAccountsCount();
    //Task<bool> ExistsCurrentAccountAsync(string currentAccountName, CancellationToken cancellation  );
    Task<ICollection<CurrentAccount>> GetAllAsync(string search, CancellationToken cancellationToken);
    Task<CurrentAccount?> GetDetailByErpRefIdAsync(string erpRefId, CancellationToken cancellationToken = default);
}
