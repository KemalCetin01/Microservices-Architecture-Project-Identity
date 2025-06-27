using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.User.DTOs
{
    public class UpdateB2CUserCommandDTO
    {
        public Guid Id { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? TownId { get; set; }
        public Guid? RepresentativeId { get; set; }
        public int? SectorId { get; set; }
        public int? ActivityAreaId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Password { get; set; }
        public string Email { get; set; } = null!;
        public DateTime? BirthDate { get; set; }
        public GenderEnum? Gender { get; set; }
        public int? OccupationId { get; set; }
        public string PhoneCountryCode { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public SiteStatusEnum SiteStatus { get; set; }
        public UserStatusEnum UserStatus { get; set; }
        public bool? ContactPermission { get; set; }
    }
}
