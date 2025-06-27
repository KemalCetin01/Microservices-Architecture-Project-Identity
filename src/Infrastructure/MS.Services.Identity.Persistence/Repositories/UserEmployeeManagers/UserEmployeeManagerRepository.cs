using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Handlers.EmployeeManagers.DTOs;
using MS.Services.Identity.Persistence.Context;

namespace MS.Services.Identity.Persistence.Repositories;
public class UserEmployeeManagerRepository : Repository<UserEmployeeManager, IdentityDbContext>, IEmployeeManagerRepository
{
    public UserEmployeeManagerRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }
    public async Task<ICollection<UserEmployeeManager>> GetAsync(Guid userId, CancellationToken cancellationToken  )
    {
        return await Queryable().Where(x => x.EmployeeId == userId).ToListAsync(cancellationToken);
    }
    public async Task<ICollection<UserEmployeeManagersDTO>> GetEmployeeManagersAsync(Guid userId, CancellationToken cancellationToken  )
    {
        var result = Queryable()
            .Where(x => x.EmployeeId == userId && x.IsDeleted == false)
            .Select(x => new UserEmployeeManagersDTO()
            {
                FullName = x.Manager.User.FirstName + " " + x.Manager.User.LastName,
                Email = x.Manager.User.Email,
                Id = x.Id,
                ManagerId = x.ManagerId
            }).AsNoTrackingWithIdentityResolution();
        return await result.ToListAsync(cancellationToken);
    }

    public async Task<bool> HasEmployeeManager(Guid userId, Guid managerId, CancellationToken cancellationToken  )
    {
        return await Queryable().AnyAsync(x => x.EmployeeId == userId && x.ManagerId == managerId && x.IsDeleted == false);
    }

    public async Task<int> ManagerUpdatesCount(Guid userId, CancellationToken cancellationToken  )
    {
        var managers = Queryable().Where(x => (x.EmployeeId == userId || x.ManagerId == userId) && x.IsDeleted == false);
        foreach (var manager in managers)
        {
            manager.IsDeleted = true;
            Update(manager);
        }
        return await managers.CountAsync(cancellationToken);
    }
}