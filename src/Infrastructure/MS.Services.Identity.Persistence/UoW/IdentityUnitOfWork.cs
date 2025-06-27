using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Persistence.Context;

namespace MS.Services.Identity.Persistence.UoW;

public class IdentityUnitOfWork : UnitOfWork<IdentityDbContext>, IIdentityUnitOfWork
{
    public IdentityUnitOfWork(IdentityDbContext dbContext) : base(dbContext)
    {
    }
}