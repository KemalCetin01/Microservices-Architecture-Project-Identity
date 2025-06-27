using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Persistence.Context;

namespace MS.Services.Identity.Persistence.Repositories;

public class UserRepository : Repository<User, IdentityDbContext>, IUserRepository
{
    public UserRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> EmailExist(string email)
    {
        return Queryable().Where(x => x.Email == email).Count() > 0 ? true : false;
    }

    public async Task<User?> GetByEmailAsync(string email, UserTypeEnum platform, CancellationToken cancellationToken)
    {
        return await Queryable().Where(x => x.Email == email && x.UserTypeId == (int)platform && !x.IsDeleted).FirstOrDefaultAsync();

    }

    public async Task<User> GetById(Guid Id, CancellationToken cancellationToken)
    {
        return await Queryable()
            .Where(x => x.Id == Id && x.IsDeleted == false).FirstOrDefaultAsync(cancellationToken);
    }

    //deleted kayıtlar default olarak gönmüyor
    public async Task<bool> AnotherUserHasEmailAsync(Guid? userId, UserTypeEnum userType, string email, CancellationToken cancellationToken)
    {
        var query = Queryable();
        if (userId != null) query = query.Where(x => x.Id != userId);
        return await query.AnyAsync(x => x.UserTypeId == (int)userType && x.Email == email, cancellationToken);
    }

}