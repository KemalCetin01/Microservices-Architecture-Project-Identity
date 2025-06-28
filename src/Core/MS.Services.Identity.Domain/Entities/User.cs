using MS.Services.Core.Data.Data.Attributes;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

public class User : BaseSoftDeleteEntity
{
    [QuerySearch]
    public string FirstName { get; set; } = null!;
    [QuerySearch]
    public string LastName { get; set; } = null!;
    [QuerySearch]
    public string Email { get; set; } = null!;
    public string? Suffix { get; set; }
    public Guid IdentityRefId { get; set; }

    public ICollection<UserConfirmRegisterType>? UserConfirmRegisterTypes { get; set; }

    public int? UserTypeId { get; set; }
    public UserType? UserType { get; set; }
    public UserTypeEnum UserTypeEnum =>(UserTypeEnum)UserTypeId;

}