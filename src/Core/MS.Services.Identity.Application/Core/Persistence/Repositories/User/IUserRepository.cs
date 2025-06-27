using MS.Services.Core.Data.Data.Interface;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User> GetById(Guid Id, CancellationToken cancellationToken);
    Task<bool> EmailExist(string email);
    Task<User?> GetByEmailAsync(string email, UserTypeEnum platform, CancellationToken cancellationToken);
    Task<bool> AnotherUserHasEmailAsync(Guid? userId, UserTypeEnum userType, string email, CancellationToken cancellationToken);
}