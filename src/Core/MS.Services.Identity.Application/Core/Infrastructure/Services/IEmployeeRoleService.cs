using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.Commands;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.DTOs;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;
public interface IEmployeeRoleService : IScopedService
{
    Task<PagedResponse<EmployeeRoleDTO>> SearchAsync(SearchQueryModel<SearchUserEmployeeRolesQueryFilterModel> searchQuery, CancellationToken cancellationToken);
    Task<ICollection<EmployeeRoleKeyValueDTO>> SearchFKeyAsync(string search, CancellationToken cancellationToken);
    Task<EmployeeRoleDTO> AddAsync(CreateEmployeeRoleCommand createEmployeeCommand, CancellationToken cancellationToken);
    Task<EmployeeRoleDTO> UpdateAsync(UpdateEmployeeRoleCommand updateEmployeeCommand, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid Id, CancellationToken cancellationToken);
    Task<EmployeeRoleDTO> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

}