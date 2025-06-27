namespace MS.Services.Identity.Persistence.EntityConfigurations;

public class UserB2BConfigurations : IEntityTypeConfiguration<UserB2B>
{
    public void Configure(EntityTypeBuilder<UserB2B> builder)
    {
        builder.ToTable(nameof(UserB2B));
        builder.HasOne(x => x.User).WithOne();
        builder.HasKey(x => x.UserId);
       
        builder.Property(x => x.PhoneCountryCode).HasMaxLength(8);
        builder.Property(x => x.Phone).HasMaxLength(15);
    }
}