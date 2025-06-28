using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Handlers.User.DTOs;
using MS.Services.Identity.Domain.Filters.UserB2BFilters;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;
public interface IUserB2BService : IScopedService
{
    Task<PagedResponse<B2BUserListDTO>> GetUsersAsync(SearchQueryModel<UserB2BQueryServiceFilter> searchQuery, CancellationToken cancellationToken);
    Task<B2BUserGetByIdDTO> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<B2BUserGetByIdDTO> CreateAsync(CreateB2BUserCommandDTO model, CancellationToken cancellationToken);
    Task<B2BUserGetByIdDTO> UpdateAsync(UpdateB2BUserCommandDTO b2BUserGetByIdDTO, CancellationToken cancellationToken);
    Task DeleteAsync(Guid Id, CancellationToken cancellationToken);
    Task<int> GetActiveUserInRoleCountAsync(Guid userGroupRoleId, CancellationToken cancellationToken);
    Task<bool> ResetPasswordAsync(ResetPasswordCommandDTO model, CancellationToken cancellationToken);
}

