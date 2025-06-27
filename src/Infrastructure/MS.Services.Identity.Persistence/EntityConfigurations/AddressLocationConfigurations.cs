using MS.Services.Identity.Domain.EntityConstants;

namespace MS.Services.Identity.Persistence.EntityConfigurations
{
    public class AddressLocationConfigurations : IEntityTypeConfiguration<AddressLocation>
    {
        public void Configure(EntityTypeBuilder<AddressLocation> builder)
        {
            builder.ToTable(nameof(AddressLocation));
            builder.Property(x => x.CountryCode).IsRequired(true).HasMaxLength(AddressLocationConstants.CountryCodeMaxLength);
            builder.Property(x => x.CityName).IsRequired(false).HasMaxLength(AddressLocationConstants.CityNameMaxLength);
            builder.Property(x => x.TownName).IsRequired(false).HasMaxLength(AddressLocationConstants.TownNameMaxLength);
            builder.Property(x => x.DistrictName).IsRequired(false).HasMaxLength(AddressLocationConstants.DistrictNameMaxLength);
            builder.Property(x => x.ZipCode).IsRequired(false).HasMaxLength(AddressLocationConstants.ZipCodeMaxLength);
            builder.Property(x => x.AddressLine1).IsRequired(false).HasMaxLength(AddressLocationConstants.AddressLineMaxLength);
            builder.Property(x => x.AddressLine2).IsRequired(false).HasMaxLength(AddressLocationConstants.AddressLineMaxLength);
            builder.Property(x => x.AddressDescription).IsRequired(false).HasMaxLength(AddressLocationConstants.AddressLineMaxLength);
        }
    }
}
