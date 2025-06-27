using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Filters.UserB2BFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IUserB2BRepository : IRepository<UserB2B>
{
    Task<SearchListModel<UserB2B>> SearchBusinessUsersAsync(SearchQueryModel<BusinessUserSearchFilter> searchQuery, CancellationToken cancellationToken);
    Task<SearchListModel<UserB2B>> GetAll(SearchQueryModel<UserB2BQueryServiceFilter> searchQuery, CancellationToken cancellationToken);
    Task<UserB2B> GetById(Guid id, CancellationToken cancellationToken);
    Task<UserB2B?> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<int> GetActiveUserInRoleCountAsync(Guid userGroupRoleId, CancellationToken cancellationToken);
    Task<List<UserB2B>> GetUsersByBusinessKeycloakId(Guid identityGroupRefId, CancellationToken cancellationToken);
}
