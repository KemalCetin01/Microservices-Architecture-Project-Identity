using MS.Services.Core.Base.Dtos;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.User.DTOs
{
    public class B2CUserGetByIdDTO : IResponse
    {
        public Guid Id { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? TownId { get; set; }
        public Guid? RepresentativeId { get; set; }
        public int? SectorId { get; set; } 
        public int? ActivityAreaId { get; set; } 
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public GenderEnum? Gender { get; set; }
        public int? OccupationId { get; set; }
        public string PhoneCountryCode { get; set; }
        public string Phone { get; set; }
        public string? CompanyName { get; set; }
        public int? PositionId { get; set; }
        public int? NumberOfEmployees { get; set; }
        public DateTime? CreatedDate { get; set; }
        public SiteStatusEnum? SiteStatus { get; set; }
        public UserStatusEnum? UserStatus { get; set; }
        public UserProfileEnum? UserProfile { get; set; }
        public bool? ContactPermission { get; set; }
    }
}
