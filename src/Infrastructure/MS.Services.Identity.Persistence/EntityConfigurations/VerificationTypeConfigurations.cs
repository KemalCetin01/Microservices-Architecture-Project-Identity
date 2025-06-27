using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Persistence.EntityConfigurations;
public class VerificationTypeConfigurations : IEntityTypeConfiguration<VerificationType>
{
    public void Configure(EntityTypeBuilder<VerificationType> builder)
    {
        builder.ToTable(nameof(VerificationType));
        builder.Property(x => x.Name)
            .HasConversion(v => v.ToString(), v => (VerificationTypeEnum)Enum.Parse(typeof(VerificationTypeEnum), v));
    }
}