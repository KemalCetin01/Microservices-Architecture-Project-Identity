namespace MS.Services.Identity.Persistence.EntityConfigurations
{
    public class BusinessShippingAddressConfigurations : IEntityTypeConfiguration<BusinessShippingAddress>
    {
        public void Configure(EntityTypeBuilder<BusinessShippingAddress> builder)
        {
            builder.ToTable(nameof(BusinessShippingAddress));
        }
    }
}
