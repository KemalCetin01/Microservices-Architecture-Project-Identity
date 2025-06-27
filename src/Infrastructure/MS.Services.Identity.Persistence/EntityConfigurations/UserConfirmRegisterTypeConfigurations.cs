using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Persistence.EntityConfigurations;

public class UserConfirmRegisterTypeConfigurations : IEntityTypeConfiguration<UserConfirmRegisterType>
{
    public void Configure(EntityTypeBuilder<UserConfirmRegisterType> builder)
    {
        builder.ToTable(nameof(UserConfirmRegisterType));
        // builder.HasIndex(x => new { x.UserId, x.ConfirmRegisterTypeId }).IsUnique();
        builder.Ignore(x=>x.ConfirmRegisterEnum);
        builder
            .HasOne(x => x.User)
            .WithMany(f => f.UserConfirmRegisterTypes)
            .HasForeignKey(x => x.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.ConfirmRegisterType)
            .WithMany()
            .HasForeignKey(x => x.ConfirmRegisterTypeId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
