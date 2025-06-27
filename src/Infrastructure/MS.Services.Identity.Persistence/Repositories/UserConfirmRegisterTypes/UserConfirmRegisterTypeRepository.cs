using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Persistence.Context;

namespace MS.Services.Identity.Persistence.Repositories;

public class UserConfirmRegisterTypeRepository : Repository<UserConfirmRegisterType, IdentityDbContext>, IUserConfirmRegisterTypeRepository
{
    public UserConfirmRegisterTypeRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }
}
