namespace MS.Services.Identity.Persistence.EntityConfigurations
{
    public class UserEmployeeManagerConfigurations : IEntityTypeConfiguration<UserEmployeeManager>
    {
        public void Configure(EntityTypeBuilder<UserEmployeeManager> builder)
        {
            builder.ToTable(nameof(UserEmployeeManager));
        }
    }
}
