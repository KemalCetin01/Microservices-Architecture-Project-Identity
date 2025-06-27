using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface IBusinessCurrentAccountService : IScopedService
{
    Task<PagedResponse<SearchBusinessCurrentAccountsResponseDto>> SearchAsync(SearchQueryModel<BusinessCurrentAccountSearchFilter> searchQuery, CancellationToken cancellationToken);
    Task AddCurrentAccountToBusinessAsync(Guid businessId, Guid currentAccountId, CancellationToken cancellationToken);
    Task RemoveCurrentAccountFromBusinessAsync(Guid businessId, Guid currentAccountId, CancellationToken cancellationToken);
}
