using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

public class UserOTP : BaseEntity<Guid>
{
    public override Guid Id { get; init; }
    public string OtpCode { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public VerificationTypeEnum VerificationType { get; set; }
    public DateTime? VerificationDate { get; set; }
    public bool IsVerified { get; set; }
    public DateTime? ExpireDate { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public DateTime? CreatedDate { get; set; }
    public OtpTypeEnum OtpType { get; set; }
    public UserTypeEnum Platform { get; set; }
    public UserResetPassword? UserResetPassword { get; set; }
}
