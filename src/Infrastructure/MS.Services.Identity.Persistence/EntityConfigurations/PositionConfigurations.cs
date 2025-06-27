namespace MS.Services.Identity.Persistence.EntityConfigurations
{
    public class PositionConfigurations : IEntityTypeConfiguration<Position>
    {
        public void Configure(EntityTypeBuilder<Position> builder)
        {
            builder.ToTable(nameof(Position));
        }
    }
}
