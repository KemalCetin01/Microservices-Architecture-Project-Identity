namespace MS.Services.Identity.Persistence.EntityConfigurations
{
    public class OccupationConfigurations : IEntityTypeConfiguration<Occupation>
    {
        public void Configure(EntityTypeBuilder<Occupation> builder)
        {
            builder.ToTable(nameof(Occupation));
        }
    }
}
