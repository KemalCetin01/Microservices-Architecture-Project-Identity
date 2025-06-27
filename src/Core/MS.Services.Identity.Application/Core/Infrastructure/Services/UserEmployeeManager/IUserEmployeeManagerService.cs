using MS.Services.Core.Base.IoC;
using MS.Services.Identity.Application.Handlers.EmployeeManagers.Commands;
using MS.Services.Identity.Application.Handlers.EmployeeManagers.DTOs;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;
public interface IUserEmployeeManagerService : IScopedService
{
    Task<ICollection<UserEmployeeManagersDTO>> GetEmployeeManagersAsync(Guid employeeId, CancellationToken cancellationToken  );
    Task<bool> AssignEmployeeManager(AssignUserEmployeeManagerCommand command, CancellationToken cancellationToken  );
}
