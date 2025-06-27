using MS.Services.Identity.Domain.EntityConstants;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Persistence.EntityConfigurations;

public class BusinessConfigurations : IEntityTypeConfiguration<Business>
{
    public void Configure(EntityTypeBuilder<Business> builder)
    {
        builder.ToTable(nameof(Business));
        builder.Property(x => x.Name).IsRequired().HasMaxLength(BusinessConstants.NameMaxLength);
        builder.Property(x => x.Code).IsRequired().HasMaxLength(BusinessConstants.NameMaxLength);
        builder.Property(x => x.DiscountRate).IsRequired().HasDefaultValue(0);
        builder.Property(x => x.IdentityRefId).IsRequired(false);
        builder.Property(x => x.Phone).IsRequired(false).HasMaxLength(BusinessConstants.PhoneMaxLength);
        builder.Property(x => x.PhoneCountryCode).IsRequired(false).HasMaxLength(BusinessConstants.PhoneCountryCodeMaxLength);
        builder.Property(x => x.FaxNumber).IsRequired(false).HasMaxLength(BusinessConstants.FaxNumberMaxLength);
        
        builder.HasOne(x => x.Representative).WithMany().IsRequired(false).HasForeignKey(x => x.RepresentativeId);
        builder.HasOne(x => x.Sector).WithMany().IsRequired(false).HasForeignKey(x => x.SectorId);
        builder.HasOne(x => x.ActivityArea).WithMany().IsRequired(false).HasForeignKey(x => x.ActivityAreaId);
        builder.HasOne(x => x.NumberOfEmployee).WithMany().IsRequired(false).HasForeignKey(x => x.NumberOfEmployeeId);
        builder.HasOne(x => x.BusinessStatus).WithMany().IsRequired(false).HasForeignKey(x => x.BusinessStatusId);
        builder.HasOne(x => x.BillingAddress).WithOne(x => x.Business).IsRequired(false);
        builder.HasMany(x => x.BusinessShippingAddresses).WithOne(x => x.Business).HasForeignKey(x => x.BusinessId).IsRequired(false);

        builder.Property(x => x.ReviewStatus).IsRequired(false).HasDefaultValue(ReviewStatusEnum.Pending);
        builder.Property(x => x.ReviewDate).IsRequired(false);
        builder.Property(x => x.ReviewBy).IsRequired(false);
        
        
        builder.HasMany(x => x.Users)
                .WithMany(x => x.Businesses)
                .UsingEntity<BusinessUser>();
             /*
nameof(BusinessUser),
                    x => x.HasOne<UserB2B>(x => x.UserB2B).WithMany(x => x.BusinessUsers).HasForeignKey(x => x.UserId),
                    x => x.HasOne<Business>(x => x.Business).WithMany(x => x.BusinessUsers).HasForeignKey(x => x.BusinessId)
*/
        builder.Ignore(x => x.BusinessStatusEnum);
        builder.Ignore(x => x.BusinessType);
    }
}