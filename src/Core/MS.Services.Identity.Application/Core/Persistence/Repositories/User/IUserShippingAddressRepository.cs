using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Filters.AddressFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IUserShippingAddressRepository : IRepository<UserShippingAddress>
{
    Task<UserShippingAddress> GetById(Guid Id, CancellationToken cancellationToken);
    Task<UserShippingAddress> GetByAddressId(Guid AddressId, CancellationToken cancellationToken);
    Task<SearchListModel<UserShippingAddress>> GetAddressByUserId(SearchQueryModel<AddressQueryServiceFilter> searchQuery, Guid? userId, CancellationToken cancellationToken);
}
