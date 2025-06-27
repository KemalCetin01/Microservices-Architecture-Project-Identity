using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Filters.AddressFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IUserBillingAddressRepository : IRepository<UserBillingAddress>
{
    Task<UserBillingAddress> GetById(Guid Id, CancellationToken cancellationToken);
    Task<UserBillingAddress> GetByAddressId(Guid AddressId, CancellationToken cancellationToken);
    Task<SearchListModel<UserBillingAddress>> GetAddressByUserId(SearchQueryModel<AddressQueryServiceFilter> searchQuery, Guid? userId, CancellationToken cancellationToken);
}
