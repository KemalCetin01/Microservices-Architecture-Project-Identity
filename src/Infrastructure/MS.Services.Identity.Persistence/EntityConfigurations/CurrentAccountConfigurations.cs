namespace MS.Services.Identity.Persistence.EntityConfigurations;

public class CurrentAccountConfigurations : IEntityTypeConfiguration<CurrentAccount>
{
    public void Configure(EntityTypeBuilder<CurrentAccount> builder)
    {
        builder.ToTable(nameof(CurrentAccount));
        builder.Property(x => x.CurrentAccountStatus).HasComment("0:Kullanımda - 1:Kullanım Dışı ");//TODO:
        builder.Property(x => x.SiteStatus).HasComment("1:Aktif - 0:Pasif");
        builder.Property(x => x.SalesAndDistribution).HasComment("1:Açık - 0:Kapalı");
        builder.Property(x => x.CurrentAccountType).HasComment("1:Alıcı - 2:Satıcı - 3:Alıcı Ve Satıcı");

        builder.HasOne(x => x.Business)
            .WithMany(x => x.CurrentAccounts)
            .IsRequired(false)
            .HasForeignKey(x => x.BusinessId);
    }
}