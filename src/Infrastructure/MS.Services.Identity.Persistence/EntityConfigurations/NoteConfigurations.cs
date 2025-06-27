namespace MS.Services.Identity.Persistence.EntityConfigurations
{
    public class NoteConfigurations : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.ToTable(nameof(Note));
            builder.HasOne(x => x.CreatedByUser).WithMany().HasForeignKey(x => x.CreatedBy).HasPrincipalKey(x => x.UserId);
        }
    }
}
