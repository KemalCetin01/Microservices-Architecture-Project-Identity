using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Filters.UserB2CFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IUserB2CRepository : IRepository<UserB2C>
{
    Task<SearchListModel<UserB2C>> GetAll(SearchQueryModel<UserB2CQueryServiceFilter> searchQuery, CancellationToken cancellationToken);
    Task<UserB2C> GetById(Guid id, CancellationToken cancellationToken);
    Task<UserB2C?> GetByPhoneAsync(string phone, CancellationToken cancellationToken);
    Task<UserB2C?> GetByEmailAsync(string email, CancellationToken cancellationToken);

    Task<bool> AnotherUserHasPhoneAsync(Guid? userId, string phoneCountryCode, string phone, CancellationToken cancellationToken);
}
