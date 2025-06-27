using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Handlers.EmployeeManagers.Commands;
using MS.Services.Identity.Application.Handlers.EmployeeManagers.DTOs;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Infrastructure.Services.EmployeeManager;
public class UserEmployeeManagerService : IUserEmployeeManagerService
{
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IEmployeeManagerRepository _employeeManagerRepository;

    public UserEmployeeManagerService(IIdentityUnitOfWork identityUnitOfWork, IEmployeeManagerRepository employeeManagerRepository)
    {
        _identityUnitOfWork = identityUnitOfWork;
        _employeeManagerRepository = employeeManagerRepository;
    }

    public async Task<ICollection<UserEmployeeManagersDTO>> GetEmployeeManagersAsync(Guid employeeId, CancellationToken cancellationToken  )
    {
        return await _employeeManagerRepository.GetEmployeeManagersAsync(employeeId, cancellationToken);

    }
    public async Task<bool> AssignEmployeeManager(AssignUserEmployeeManagerCommand command, CancellationToken cancellationToken  )
    {
        /* İş Kuralı (Toplu Yönetici Atama)
         Soft Delete yapılacağı için;
        1->tüm yöneticiler önce soft delete yapılır. 
        2->tanımlanan yöneticiler yeni ise AddRange yapılır
        3->tanımlanan yöneticiler daha önceden var olup silinen arasındaysa isDeleted tekrar false yapılır
         */

        if (command.ManagerId.Contains(command.EmployeeId))
            throw new ApiException(EmployeeManagerConstants.EmployeeManagerAssignYourselfError);

        var transactionCount = 0;
        var managers = new List<Domain.Entities.UserEmployeeManager>() { };

        var employeeManagers = await _employeeManagerRepository.GetAsync(command.EmployeeId, cancellationToken);

        foreach (var userManagers in employeeManagers)
        {
            userManagers.IsDeleted = true;
            _employeeManagerRepository.Update(userManagers);
            transactionCount++;
        }
        transactionCount += command.ManagerId.Count;

        foreach (var managerId in command.ManagerId)
        {
            if (!employeeManagers.Any(x => x.ManagerId == managerId))
            {
                managers.Add(new Domain.Entities.UserEmployeeManager() { EmployeeId = command.EmployeeId, ManagerId = managerId });
            }
            else
            {
                var currentManager = employeeManagers.Where(x => x.ManagerId == managerId).FirstOrDefault();
                if (currentManager != null)
                {
                    currentManager.IsDeleted = false;

                    _employeeManagerRepository.Update(currentManager);
                    transactionCount--;
                }
            }
        }

        if (transactionCount > 0)
        {
            await _employeeManagerRepository.AddRangeAsync(managers, cancellationToken);

            var transaction = await _identityUnitOfWork.CommitAsync(cancellationToken);

            return transaction != transactionCount
                ? false
                : true;
        }
        return true;
    }
}
