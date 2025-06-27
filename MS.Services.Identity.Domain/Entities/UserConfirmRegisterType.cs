using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

public class UserConfirmRegisterType : BaseGuidEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int ConfirmRegisterTypeId { get; set; }
    public ConfirmRegisterType ConfirmRegisterType { get; set; }
    public ConfirmRegisterEnum ConfirmRegisterEnum => (ConfirmRegisterEnum)ConfirmRegisterTypeId;
}
