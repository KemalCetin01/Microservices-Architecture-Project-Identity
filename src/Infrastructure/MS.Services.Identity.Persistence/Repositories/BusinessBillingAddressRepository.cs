using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Domain.Filters.AddressFilters;
using MS.Services.Identity.Persistence.Context;

namespace MS.Services.Identity.Persistence.Repositories;

public class BusinessBillingAddressRepository : Repository<BusinessBillingAddress, IdentityDbContext>, IBusinessBillingAddressRepository
{
    public BusinessBillingAddressRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<BusinessBillingAddress?> GetById(Guid Id, CancellationToken cancellationToken)
    {
        return await Queryable()
           .Include(x => x.AddressLocation)
           .Where(x => x.Id == Id)
           .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<BusinessBillingAddress?> GetByBusinessId(Guid businessId, CancellationToken cancellationToken)
    {
        return await Queryable()
            .Include(x => x.AddressLocation)
            .Where(x => x.BusinessId == businessId)
            .FirstOrDefaultAsync(cancellationToken);
    }
}