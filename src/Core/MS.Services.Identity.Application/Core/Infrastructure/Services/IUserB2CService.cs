using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Handlers.User.DTOs;
using MS.Services.Identity.Domain.Filters.UserB2CFilters;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;
public interface IUserB2CService : IScopedService
{
    Task<PagedResponse<B2CUserListDTO>> GetUsersAsync(SearchQueryModel<UserB2CQueryServiceFilter> searchQuery, CancellationToken cancellationToken);
    Task<B2CUserGetByIdDTO> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<B2CUserGetByIdDTO> CreateAsync(CreateB2CUserCommandDTO model, CancellationToken cancellationToken);
    Task<B2CUserGetByIdDTO> UpdateAsync(UpdateB2CUserCommandDTO model, CancellationToken cancellationToken);
    Task DeleteAsync(Guid Id, CancellationToken cancellationToken);
    Task<bool> ResetPasswordAsync(ResetPasswordCommandDTO model, CancellationToken cancellationToken);
}