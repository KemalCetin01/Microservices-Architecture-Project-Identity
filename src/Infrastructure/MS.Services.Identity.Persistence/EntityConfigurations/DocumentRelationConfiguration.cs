namespace MS.Services.Identity.Persistence.EntityConfigurations;

public class DocumentRelationConfiguration : IEntityTypeConfiguration<DocumentRelation>
{
    public void Configure(EntityTypeBuilder<DocumentRelation> builder)
    {
        builder.ToTable(nameof(DocumentRelation));

        builder.HasOne(x => x.Document)
            .WithMany(x => x.DocumentRelations)
            .HasForeignKey(x => x.DocumentId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(x => x.Entity)
            .IsRequired();

        builder.Property(x => x.EntityId)
            .IsRequired();

        builder.Property(x => x.EntityField)
            .IsRequired();

        builder.Property(x => x.Order)
            .IsRequired();
    }
}