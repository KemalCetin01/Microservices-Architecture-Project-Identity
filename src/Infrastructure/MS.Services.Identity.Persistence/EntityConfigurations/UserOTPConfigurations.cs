using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Persistence.EntityConfigurations;

public class UserOTPConfigurations : IEntityTypeConfiguration<UserOTP>
{
    public void Configure(EntityTypeBuilder<UserOTP> builder)
    {
        builder.ToTable(nameof(UserOTP));
        builder.Property(x => x.VerificationType).HasDefaultValue(VerificationTypeEnum.Email).HasComment("1:email - 2:phone");
        builder.Property(x => x.Platform).HasDefaultValue(UserTypeEnum.B2C).HasComment("1:b2b - 2:b2c - 3:employee");
        builder.Property(x => x.OtpType).HasDefaultValue(OtpTypeEnum.SignUp).HasComment("1:signUp - 2:ResetPassword - 3:CreatePassword");
        builder.Property(x => x.Phone).HasMaxLength(15);
    }
}
