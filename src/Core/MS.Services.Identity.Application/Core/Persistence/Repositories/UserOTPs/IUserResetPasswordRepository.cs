using MS.Services.Core.Data.Data.Interface;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IUserResetPasswordRepository : IRepository<UserResetPassword>
{
    Task<UserResetPassword> GetVerifyByIdAsync(Guid transactionId, CancellationToken cancellationToken);
}
