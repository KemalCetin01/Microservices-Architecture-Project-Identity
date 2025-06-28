using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Persistence.Context;
using MS.Services.Identity.Persistence.Helpers;
using MS.Services.Identity.Persistence.Repositories;
using MS.Services.Identity.Persistence.UoW;

namespace MS.Services.Identity.Persistence;

public static class ServiceRegistrations
{
    public static void AddPersistenceLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
    {

        serviceCollection.AddDbContext<IdentityDbContext>(opt =>
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                x => x.MigrationsAssembly("MS.Services.Identity.DbMigrations")));

        serviceCollection.AddScoped<IdentityDbContextOption>();
        serviceCollection.AddScoped<IIdentityUnitOfWork, IdentityUnitOfWork>();
        serviceCollection.AddScoped<IUserRepository, UserRepository>();
        serviceCollection.AddScoped<ICurrentAccountRepository, CurrentAccountRepository>();
        serviceCollection.AddScoped<IUserEmployeeRepository, UserEmployeeRepository>();
        serviceCollection.AddScoped<IEmployeeRoleRepository, EmployeeRoleRepository>();
        serviceCollection.AddScoped<ISectorRepository, SectorRepository>();
        serviceCollection.AddScoped<IIdentityIdSequenceRepository, IdentityIdSequenceRepository>();
        serviceCollection.AddScoped<IUserOTPRepository, UserOTPRepository>();
        serviceCollection.AddScoped<IUserResetPasswordRepository, UserResetPasswordRepository>();
        serviceCollection.AddScoped<IUserConfirmRegisterTypeRepository, UserConfirmRegisterTypeRepository>();


    }
}