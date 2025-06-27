using MS.Services.Identity.Domain.EntityConstants;

namespace MS.Services.Identity.Persistence.EntityConfigurations
{
    public class BusinessBillingAddressConfigurations : IEntityTypeConfiguration<BusinessBillingAddress>
    {
        public void Configure(EntityTypeBuilder<BusinessBillingAddress> builder)
        {
            builder.ToTable(nameof(BusinessBillingAddress));
            builder.HasOne(x => x.Business)
                    .WithOne(x => x.BillingAddress)
                    .HasForeignKey<BusinessBillingAddress>(x=>x.BusinessId)
                    .IsRequired(false);
            builder.Property(x => x.BillingType).IsRequired();
            
            builder.Property(x => x.Email).IsRequired(false).HasMaxLength(BaseAddressConstants.EmailMaxLength);
            builder.Property(x => x.FirstName).IsRequired(false).HasMaxLength(BaseAddressConstants.FirstNameMaxLength);
            builder.Property(x => x.LastName).IsRequired(false).HasMaxLength(BaseAddressConstants.LastNameMaxLength);
            builder.Property(x => x.CompanyName).IsRequired(false).HasMaxLength(BaseAddressConstants.CompanyNameMaxLength);
            builder.Property(x => x.IdentityNumber).IsRequired(false).HasMaxLength(BaseAddressConstants.IdentityNumberMaxLength);
            builder.Property(x => x.TaxNumber).IsRequired(false).HasMaxLength(BaseAddressConstants.TaxNumberMaxLength);
            builder.Property(x => x.TaxOffice).IsRequired(false).HasMaxLength(BaseAddressConstants.TaxOfficeMaxLength);
        }
    }

    
}
