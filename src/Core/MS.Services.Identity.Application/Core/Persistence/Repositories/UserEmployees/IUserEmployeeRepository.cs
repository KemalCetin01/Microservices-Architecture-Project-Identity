using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IUserEmployeeRepository : IRepository<UserEmployee>
{
    Task<ICollection<LabelValueModel>> GetEmployees(string search, CancellationToken cancellationToken  );
    Task<UserEmployee> GetByIdAsync(Guid Id, CancellationToken cancellationToken  );
    Task<SearchListModel<UserEmployee>> SearchAsync(SearchQueryModel<SearchUserEmployeesQueryFilterModel> searchQuery, CancellationToken cancellationToken);
    Task<int> GetActiveUserInRoleCountAsync(Guid roleId, CancellationToken cancellationToken  );
}
