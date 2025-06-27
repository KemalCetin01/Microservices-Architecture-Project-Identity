namespace MS.Services.Identity.Persistence.EntityConfigurations
{
    public class ActivityAreaConfigurations : IEntityTypeConfiguration<ActivityArea>
    {
        public void Configure(EntityTypeBuilder<ActivityArea> builder)
        {
            builder.ToTable(nameof(ActivityArea));
        }
    }
}
