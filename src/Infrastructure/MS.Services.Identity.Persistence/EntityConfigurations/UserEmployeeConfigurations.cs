namespace MS.Services.Identity.Persistence.EntityConfigurations;
public class UserEmployeeConfigurations : IEntityTypeConfiguration<UserEmployee>
{
    public void Configure(EntityTypeBuilder<UserEmployee> builder)
    {
        builder.ToTable(nameof(UserEmployee));
        builder.HasOne(x => x.User).WithOne();
        builder.HasKey(x => x.UserId);

        builder.HasMany(x => x.EmployeeManagers)
            .WithOne(x => x.UserEmployee)
        .HasForeignKey(x => x.EmployeeId)
        .HasPrincipalKey(x => x.UserId);

    }
}