using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MS.Services.Identity.Persistence.EntityConfigurations;

public class BusinessStatusConfigurations : IEntityTypeConfiguration<BusinessStatus>
{
    public void Configure(EntityTypeBuilder<BusinessStatus> builder)
    {
        builder.ToTable(nameof(BusinessStatus));

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(64)
            .IsRequired();
    }
}