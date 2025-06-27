using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Persistence.EntityConfigurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(nameof(User));
        builder.Ignore(x=>x.UserTypeEnum);
        //builder.Ignore(x=>x.UserType);
        //builder.HasIndex(x => new { x.UserType, x.Email,x.IsDeleted }).IsUnique();
        //builder.HasOne(x => x.UserType).WithMany(x=>x.Users);
    }
}
