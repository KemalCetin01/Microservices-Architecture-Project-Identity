using MS.Services.Core.Base.Extentions;
using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Domain.Filters.UserB2BFilters;
using MS.Services.Identity.Persistence.Context;
using MS.Services.Identity.Persistence.Extensions;

namespace MS.Services.Identity.Persistence.Repositories;

public class BusinessUserRepository : Repository<BusinessUser, IdentityDbContext>, IBusinessUserRepository
{
    public BusinessUserRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }
}
