using AutoMapper;
using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Persistence.Context;

namespace MS.Services.Identity.Persistence.Repositories;

public class AddressLocationRepository : Repository<AddressLocation, IdentityDbContext>, IAddressLocationRepository
{
    private readonly IMapper _mapper;
    public AddressLocationRepository(IdentityDbContext dbContext, IMapper mapper) : base(dbContext)
    {
        _mapper = mapper;
    }

    public async Task<AddressLocation?> GetById(Guid Id, CancellationToken cancellationToken)
    {
        return await Queryable()
                    .Where(x => x.Id == Id && x.IsDeleted == false)
                    .FirstOrDefaultAsync(cancellationToken);
    }
}
