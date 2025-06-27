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
        serviceCollection.AddScoped<IActivityAreaRepository, ActivityAreaRepository>();
        serviceCollection.AddScoped<IBusinessRepository, BusinessRepository>();
        serviceCollection.AddScoped<IBusinessBillingAddressRepository, BusinessBillingAddressRepository>();
        serviceCollection.AddScoped<INoteRepository, NotesRepository>();
        serviceCollection.AddScoped<IBusinessUserRepository, BusinessUserRepository>();
        serviceCollection.AddScoped<IEmployeeManagerRepository, UserEmployeeManagerRepository>();
        serviceCollection.AddScoped<IUserEmployeeRepository, UserEmployeeRepository>();
        serviceCollection.AddScoped<IEmployeeRoleRepository, EmployeeRoleRepository>();
        serviceCollection.AddScoped<IAddressLocationRepository, AddressLocationRepository>();
        serviceCollection.AddScoped<IUserBillingAddressRepository, UserBillingAddressRepository>();
        serviceCollection.AddScoped<IUserShippingAddressRepository, UserShippingAddressRepository>();
        serviceCollection.AddScoped<IUserB2BRepository, UserB2BRepository>();
        serviceCollection.AddScoped<IUserB2CRepository, UserB2CRepository>();
        serviceCollection.AddScoped<IUserNoteRepository, UserNoteRepository>();
        serviceCollection.AddScoped<ICurrentAccountNoteRepository, CurrentAccountNoteRepository>();
        serviceCollection.AddScoped<IBusinessNoteRepository, BusinessNoteRepository>();
        serviceCollection.AddScoped<IPositionRepository, PositionRepository>();
        serviceCollection.AddScoped<IOccupationRepository, OccupationRepository>();
        serviceCollection.AddScoped<ISectorRepository, SectorRepository>();
        serviceCollection.AddScoped<INumberOfEmployeeRepository, NumberOfEmployeeRepository>();
        serviceCollection.AddScoped<IIdentityIdSequenceRepository, IdentityIdSequenceRepository>();
        serviceCollection.AddScoped<IUserOTPRepository, UserOTPRepository>();
        serviceCollection.AddScoped<IUserResetPasswordRepository, UserResetPasswordRepository>();
        serviceCollection.AddScoped<IUserConfirmRegisterTypeRepository, UserConfirmRegisterTypeRepository>();
        serviceCollection.AddScoped<IDocumentRepository, DocumentRepository>();
        serviceCollection.AddScoped<IDocumentRelationRepository, DocumentRelationRepository>();


    }
}