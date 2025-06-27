namespace MS.Services.Identity.Persistence.EntityConfigurations;

public class BusinessUserConfigurations : IEntityTypeConfiguration<BusinessUser>
{
    public void Configure(EntityTypeBuilder<BusinessUser> builder)
    {
       builder.ToTable(nameof(BusinessUser));
       builder.HasOne(x => x.UserB2B).WithMany(x=>x.BusinessUsers).HasForeignKey(x => x.UserId);
       builder.HasOne(x => x.Business).WithMany(x=>x.BusinessUsers).HasForeignKey(x => x.BusinessId);
    }
}