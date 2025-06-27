namespace MS.Services.Identity.Persistence.EntityConfigurations
{
    public class NumberOfEmployeeConfigurations : IEntityTypeConfiguration<NumberOfEmployee>
    {
        public void Configure(EntityTypeBuilder<NumberOfEmployee> builder)
        {
            builder.ToTable(nameof(NumberOfEmployee));
        }
    }
}
