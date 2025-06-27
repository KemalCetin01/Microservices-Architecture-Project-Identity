namespace MS.Services.Identity.Persistence.EntityConfigurations
{
    public class UserShippingAddressConfigurations : IEntityTypeConfiguration<UserShippingAddress>
    {
        public void Configure(EntityTypeBuilder<UserShippingAddress> builder)
        {
            builder.ToTable(nameof(UserShippingAddress));
        }
    }
}
