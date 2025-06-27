using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Filters.AddressFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IAddressLocationRepository : IRepository<AddressLocation>
{
    Task<AddressLocation?> GetById(Guid Id, CancellationToken cancellationToken  );
}
