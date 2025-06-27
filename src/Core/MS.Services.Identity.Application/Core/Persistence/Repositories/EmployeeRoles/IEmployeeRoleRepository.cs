using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Handlers.EmployeeRoles.DTOs;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;
public interface IEmployeeRoleRepository : IRepository<EmployeeRole>
{
    Task<bool> HasRoleAsync(string? name, Guid? id, CancellationToken cancellationToken  );
    Task<SearchListModel<EmployeeRole>> SearchAsync(SearchQueryModel<SearchUserEmployeeRolesQueryFilterModel> searchQuery, CancellationToken cancellationToken);
    Task<ICollection<EmployeeRoleKeyValueDTO>> SearchFKeyAsync(string search, CancellationToken cancellationToken);
}

