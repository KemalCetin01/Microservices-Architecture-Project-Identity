using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Filters.AddressFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IBusinessBillingAddressRepository : IRepository<BusinessBillingAddress>
{
    Task<BusinessBillingAddress?> GetById(Guid Id, CancellationToken cancellationToken);
    Task<BusinessBillingAddress?> GetByBusinessId(Guid businessId, CancellationToken cancellationToken);
}
