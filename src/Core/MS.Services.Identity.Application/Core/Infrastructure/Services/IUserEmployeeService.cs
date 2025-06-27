using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Handlers.UserEmployees.Commands;
using MS.Services.Identity.Application.Handlers.UserEmployees.DTOs;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface IUserEmployeeService : IScopedService
{
    Task<ICollection<LabelValueModel>> GetEmployees(string search, CancellationToken cancellationToken  );
    Task<PagedResponse<UserEmployeeDTO>> SearchAsync(SearchQueryModel<SearchUserEmployeesQueryFilterModel> searchQuery, CancellationToken cancellationToken  );
    Task<UserEmployeeDTO> GetByIdAsync(Guid Id, CancellationToken cancellationToken  );
    Task<UserEmployeeDTO> CreateAsync(CreateUserEmployeeCommandDTO createEmployeeCommand, CancellationToken cancellationToken  );
    Task<UserEmployeeDTO> UpdateAsync(UpdateUserEmployeeCommand updateEmployeeCommand, CancellationToken cancellationToken  );
    Task DeleteAsync(Guid Id, CancellationToken cancellationToken  );
    Task<int> GetActiveUserInRoleCountAsync(Guid roleId, CancellationToken cancellationToken  );
}
