using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Persistence.Context;

namespace MS.Services.Identity.Persistence.Repositories;

public class UserOTPRepository : Repository<UserOTP, IdentityDbContext>, IUserOTPRepository
{
    public UserOTPRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<UserOTP> GetVerifyByIdAsync(Guid transactionId, UserTypeEnum userType, OtpTypeEnum otpType, CancellationToken cancellationToken)
    {
        var now = DateTime.UtcNow;
        return await Queryable().Include(x=>x.UserResetPassword).Where(x => !x.IsVerified && x.Platform == userType && x.OtpType == otpType && x.Id == transactionId && x.ExpireDate > now).OrderByDescending(x => x.Id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<bool> IsVerifiedAsync(Guid userId, UserTypeEnum userType, OtpTypeEnum otpType, CancellationToken cancellationToken)
    {
        return await Queryable().AnyAsync(x => x.IsVerified && x.Platform == userType && x.OtpType == otpType && x.UserId == userId && x.VerificationDate != null, cancellationToken);
    }
}
