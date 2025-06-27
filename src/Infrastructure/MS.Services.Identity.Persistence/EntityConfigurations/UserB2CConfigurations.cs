namespace MS.Services.Identity.Persistence.EntityConfigurations;

public class UserB2CConfigurations : IEntityTypeConfiguration<UserB2C>
{
    public void Configure(EntityTypeBuilder<UserB2C> builder)
    {
        builder.ToTable(nameof(UserB2C));
        builder.HasOne(x => x.User).WithOne();
        builder.HasKey(x => x.UserId);

        builder.Property(x => x.PhoneCountryCode).HasMaxLength(8);
        builder.Property(x => x.Phone).HasMaxLength(15);
    }
}