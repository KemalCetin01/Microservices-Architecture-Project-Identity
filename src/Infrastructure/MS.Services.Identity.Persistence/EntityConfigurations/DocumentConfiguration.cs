namespace MS.Services.Identity.Persistence.EntityConfigurations;

public class DocumentConfiguration : IEntityTypeConfiguration<Document>
{
    public void Configure(EntityTypeBuilder<Document> builder)
    {
        builder.ToTable(nameof(Document));

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.IsPrivate)
            .IsRequired();
    }
}