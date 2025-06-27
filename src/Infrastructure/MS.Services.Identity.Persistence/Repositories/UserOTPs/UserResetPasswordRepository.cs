using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Persistence.Context;

namespace MS.Services.Identity.Persistence.Repositories;

public class UserResetPasswordRepository : Repository<UserResetPassword, IdentityDbContext>, IUserResetPasswordRepository
{
    public UserResetPasswordRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<UserResetPassword> GetVerifyByIdAsync(Guid transactionId, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        return await Queryable().Include(x => x.UserOTP).Where(x => !x.IsUsed && x.Id == transactionId && x.UserOTP.OtpType == OtpTypeEnum.ResetPassword && x.ExpireDate >now).OrderByDescending(x => x.Id).FirstOrDefaultAsync(cancellationToken);
    }
}
