using MS.Services.Core.Data.Data.Interface;
using MS.Services.Identity.Application.Handlers.EmployeeManagers.DTOs;
using MS.Services.Identity.Domain.Entities;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;
public interface IEmployeeManagerRepository : IRepository<UserEmployeeManager>
{
    Task<ICollection<UserEmployeeManager>> GetAsync(Guid employeeID, CancellationToken cancellationToken  );
    Task<int> ManagerUpdatesCount(Guid EmployeeId, CancellationToken cancellationToken  );
    Task<ICollection<UserEmployeeManagersDTO>> GetEmployeeManagersAsync(Guid employeeId, CancellationToken cancellationToken  );
    Task<bool> HasEmployeeManager(Guid employeeId, Guid ManagerId, CancellationToken cancellationToken  );

}
