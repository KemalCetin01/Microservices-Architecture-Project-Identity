namespace MS.Services.Identity.Persistence.EntityConfigurations
{
    public class UserBillingAddressConfigurations : IEntityTypeConfiguration<UserBillingAddress>
    {
        public void Configure(EntityTypeBuilder<UserBillingAddress> builder)
        {
            builder.ToTable(nameof(UserBillingAddress));
        }
    }
}
