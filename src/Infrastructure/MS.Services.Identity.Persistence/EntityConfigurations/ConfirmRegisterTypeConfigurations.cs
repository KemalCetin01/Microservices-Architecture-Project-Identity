namespace MS.Services.Identity.Persistence.EntityConfigurations
{
    public class ConfirmRegisterTypeConfigurations : IEntityTypeConfiguration<ConfirmRegisterType>
    {
        public void Configure(EntityTypeBuilder<ConfirmRegisterType> builder)
        {
            builder.ToTable(nameof(ConfirmRegisterType));
        }
    }
}
