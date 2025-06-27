using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

public class UserB2C : IEntity,ISoftDeleteEntity
{
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
    public DateTime? BirthDate { get; set; }
    public GenderEnum? Gender { get; set; }
    public string PhoneCountryCode { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public Guid? UserEmployeeId { get; set; }
    public UserEmployee? UserEmployee { get; set; }
    public int? CountryId { get; set; }
    //public Country? Country { get; set; }
    public int? CityId { get; set; }
    //public City? City { get; set; }
    public int? TownId { get; set; }
    //public Town? Town { get; set; }
    public int? OccupationId { get; set; }
    public Occupation? Occupation { get; set; }
    public int? SectorId { get; set; }
    public Sector? Sector { get; set; }
    public int? ActivityAreaId { get; set; }
    public ActivityArea? ActivityArea { get; set; }
    public SiteStatusEnum SiteStatus { get; set; }
    public UserStatusEnum UserStatus { get; set; }
    public bool? ContactPermission { get; set; }
    public bool IsDeleted { get; set; }

    public DateTime? DeletedDate { get; set; }

    public Guid? DeletedBy { get; set; }
}
