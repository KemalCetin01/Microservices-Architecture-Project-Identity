using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

public class UserB2B : IEntity,ISoftDeleteEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public string PhoneCountryCode { get; set; }
    public string Phone { get; set; }
    public Guid? UserEmployeeId { get; set; }
    public UserEmployee? UserEmployee { get; set; }
    public int? CountryId { get; set; }
    public int? CityId { get; set; }
    public int? TownId { get; set; }
    public int? SectorId { get; set; }
    public Sector? Sector { get; set; }
    public int? ActivityAreaId { get; set; }
    public SiteStatusEnum SiteStatus { get; set; }
    public UserStatusEnum UserStatus { get; set; }
    public Guid? UserGroupRoleId { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime? DeletedDate { get; set; }
    public Guid? DeletedBy { get; set; }
}
