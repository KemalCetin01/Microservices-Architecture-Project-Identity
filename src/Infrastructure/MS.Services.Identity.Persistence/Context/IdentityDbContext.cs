using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Interface;
using MS.Services.Identity.Persistence.DataSeeds;
//using MS.Services.Core.Localization.Models;
using MS.Services.Identity.Persistence.EntityConfigurations;

namespace MS.Services.Identity.Persistence.Context;

public class IdentityDbContext : AppDbContext
{
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options, ICurrentUserService currentUserService) : base(options, currentUserService)
    {
    }

    #region DbSet
    
    protected DbSet<CurrentAccount> CurrentAccounts { get; set; } = null!;
    protected DbSet<Business> Businesses { get; set; } = null!;
    protected DbSet<BusinessStatus> BusinessStatuses { get; set; } = null!;
    protected DbSet<User> Users { get; set; } = null!;
    protected DbSet<UserEmployee> UserEmployees { get; set; } = null!;
    protected DbSet<AddressLocation> AddressLocations { get; set; } = null!;
    protected DbSet<UserBillingAddress> UserBillingAddresses { get; set;} = null!;
    protected DbSet<UserShippingAddress> UserShippingAddresses { get; set;} = null!;
    protected DbSet<BusinessBillingAddress> BusinessBillingAddresses { get; set;} = null!;
    protected DbSet<BusinessShippingAddress> BusinessShippingAddresses { get; set;} = null!; 
    protected DbSet<ActivityArea> ActivityAreas { get; set; } = null!;
    protected DbSet<Note> Note { get; set; } = null!;
    protected DbSet<UserNote> UserNotes { get; set; } = null!;
    protected DbSet<BusinessNote> BusinessNotes { get; set; } = null!; 
    protected DbSet<BusinessUser> BusinessUsers { get; set; } = null!;
    protected DbSet<CurrentAccountNote> CurrentAccountNotes { get; set; } = null!;
    protected DbSet<UserEmployeeManager> EmployeeManagers { get; set; } = null!;
    protected DbSet<EmployeeRole> EmployeeRoles { get; set; } = null!;
    protected DbSet<UserB2B> UserB2Bs { get; set; } = null!;
    protected DbSet<UserB2C> UserB2Cs { get; set; } = null!;
    public DbSet<Sector> Sectors { get; set; } = null!;
    public DbSet<NumberOfEmployee> NumberOfEmployees { get; set; } = null!;
    public DbSet<Occupation> Occupations { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<IdentityIdSequence> IdentityIdSequences { get; set; } = null!;
    public DbSet<UserOTP> UserOTPs { get; set; } = null!;
    public DbSet<UserResetPassword> UserResetPasswords { get; set; } = null!;
    public DbSet<ConfirmRegisterType> ConfirmRegisterTypes { get; set; } = null!;
    public DbSet<UserConfirmRegisterType> UserConfirmRegisterTypes { get; set; } = null!;
    public DbSet<Document> Documents { get; set; } = null!;
    public DbSet<DocumentRelation> DocumentRelations { get; set; } = null!;
    public DbSet<UserType> UserTypes { get; set; } = null!;

    #endregion
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ActivityAreaConfigurations());
        modelBuilder.ApplyConfiguration(new AddressLocationConfigurations());
        modelBuilder.ApplyConfiguration(new BusinessBillingAddressConfigurations());
        modelBuilder.ApplyConfiguration(new BusinessConfigurations());
        modelBuilder.ApplyConfiguration(new BusinessShippingAddressConfigurations());
        modelBuilder.ApplyConfiguration(new BusinessUserConfigurations());
        modelBuilder.ApplyConfiguration(new ConfirmRegisterTypeConfigurations());
        modelBuilder.ApplyConfiguration(new CurrentAccountConfigurations());
        modelBuilder.ApplyConfiguration(new DocumentConfiguration());
        modelBuilder.ApplyConfiguration(new DocumentRelationConfiguration());
        modelBuilder.ApplyConfiguration(new EmployeeRoleConfigurations());
        modelBuilder.ApplyConfiguration(new IdentityIdSequenceConfiguration());
       // modelBuilder.ApplyConfiguration(new LocalizationConfigurations());
        modelBuilder.ApplyConfiguration(new NoteConfigurations());
        modelBuilder.ApplyConfiguration(new NumberOfEmployeeConfigurations());
        modelBuilder.ApplyConfiguration(new OccupationConfigurations());
        modelBuilder.ApplyConfiguration(new PositionConfigurations());
        modelBuilder.ApplyConfiguration(new SectorConfigurations());
        modelBuilder.ApplyConfiguration(new UserB2BConfigurations());
        modelBuilder.ApplyConfiguration(new UserB2CConfigurations());
        modelBuilder.ApplyConfiguration(new UserBillingAddressConfigurations());
        modelBuilder.ApplyConfiguration(new UserConfigurations());
        modelBuilder.ApplyConfiguration(new UserConfirmRegisterTypeConfigurations());
        modelBuilder.ApplyConfiguration(new UserEmployeeConfigurations());
        modelBuilder.ApplyConfiguration(new UserEmployeeManagerConfigurations());
        modelBuilder.ApplyConfiguration(new UserOTPConfigurations());
        modelBuilder.ApplyConfiguration(new UserResetPasswordConfigurations());
        modelBuilder.ApplyConfiguration(new UserShippingAddressConfigurations());
        modelBuilder.ApplyConfiguration(new VerificationTypeConfigurations());

        //enum entities
        modelBuilder.ApplyConfiguration(new BusinessStatusConfigurations());
        modelBuilder.ApplyConfiguration(new UserTypeConfigurations());

        IdentityKeyData.SeedIdentityKeyDatas(modelBuilder);
        BusinessStatusData.SeedBusinessStatusData(modelBuilder);
    }
}