using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Interface;
using MS.Services.Identity.Persistence.DataSeeds;
using MS.Services.Identity.Persistence.EntityConfigurations;

namespace MS.Services.Identity.Persistence.Context;

public class IdentityDbContext : AppDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options, ICurrentUserService currentUserService) : base(options, currentUserService)
    {
    }

    #region DbSet
    
    protected DbSet<CurrentAccount> CurrentAccounts { get; set; } = null!;
    protected DbSet<BusinessStatus> BusinessStatuses { get; set; } = null!;
    protected DbSet<User> Users { get; set; } = null!;
    protected DbSet<UserEmployee> UserEmployees { get; set; } = null!;
    protected DbSet<UserEmployeeManager> EmployeeManagers { get; set; } = null!;
    protected DbSet<EmployeeRole> EmployeeRoles { get; set; } = null!;
    public DbSet<Sector> Sectors { get; set; } = null!;
    public DbSet<IdentityIdSequence> IdentityIdSequences { get; set; } = null!;
    public DbSet<UserOTP> UserOTPs { get; set; } = null!;
    public DbSet<UserResetPassword> UserResetPasswords { get; set; } = null!;
    public DbSet<ConfirmRegisterType> ConfirmRegisterTypes { get; set; } = null!;
    public DbSet<UserConfirmRegisterType> UserConfirmRegisterTypes { get; set; } = null!;
    public DbSet<UserType> UserTypes { get; set; } = null!;

    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ConfirmRegisterTypeConfigurations());
        modelBuilder.ApplyConfiguration(new CurrentAccountConfigurations());
        modelBuilder.ApplyConfiguration(new EmployeeRoleConfigurations());
        modelBuilder.ApplyConfiguration(new IdentityIdSequenceConfiguration());
        modelBuilder.ApplyConfiguration(new SectorConfigurations());
        modelBuilder.ApplyConfiguration(new UserB2BConfigurations());
        modelBuilder.ApplyConfiguration(new UserConfigurations());
        modelBuilder.ApplyConfiguration(new UserConfirmRegisterTypeConfigurations());
        modelBuilder.ApplyConfiguration(new UserEmployeeConfigurations());
        modelBuilder.ApplyConfiguration(new UserOTPConfigurations());
        modelBuilder.ApplyConfiguration(new UserResetPasswordConfigurations());
        modelBuilder.ApplyConfiguration(new VerificationTypeConfigurations());

        //enum entities
        modelBuilder.ApplyConfiguration(new BusinessStatusConfigurations());
        modelBuilder.ApplyConfiguration(new UserTypeConfigurations());

        IdentityKeyData.SeedIdentityKeyDatas(modelBuilder);
        BusinessStatusData.SeedBusinessStatusData(modelBuilder);
    }
}