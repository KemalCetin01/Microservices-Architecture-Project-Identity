using Microsoft.EntityFrameworkCore;
using MS.Services.Core.Base.IoC;
using MS.Services.Identity.Domain.Entities;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;
public interface IBusinesUserRepository 
{
    DbSet<BusinessUser> Table { get; }
}
