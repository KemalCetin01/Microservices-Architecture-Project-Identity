namespace MS.Services.Identity.Persistence.EntityConfigurations
{
    public class SectorConfigurations : IEntityTypeConfiguration<Sector>
    {
        public void Configure(EntityTypeBuilder<Sector> builder)
        {
            builder.ToTable(nameof(Sector));
        }
    }
}
