namespace MS.Services.Identity.Domain.Entities;

public class UserResetPassword : BaseEntity<Guid>
{
    public override Guid Id { get; init; }
    public Guid UserOtpId{ get; set; }
    public UserOTP UserOTP { get; set; }
    public bool IsUsed { get; set; }
    public DateTime? ExpireDate { get; set; }
    public DateTime? CreatedDate { get; set; }
    public Guid? UserId { get; set; }
    public User? User { get; set; }
    public DateTime? ResetPasswordDate { get; set; }

}
