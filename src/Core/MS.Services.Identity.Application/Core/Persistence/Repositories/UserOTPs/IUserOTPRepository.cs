using MS.Services.Core.Data.Data.Interface;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IUserOTPRepository : IRepository<UserOTP>
{
    Task<UserOTP> GetVerifyByIdAsync(Guid transactionId, UserTypeEnum userType, OtpTypeEnum otpType, CancellationToken cancellationToken);
    Task<bool> IsVerifiedAsync(Guid userId, UserTypeEnum userType, OtpTypeEnum otpType, CancellationToken cancellationToken);
}
