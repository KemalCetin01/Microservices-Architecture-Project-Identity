using MS.Services.Core.Base.Dtos;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.User.DTOs
{
    public class B2BUserGetByIdDTO : IResponse
    {
        public Guid Id { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? TownId { get; set; }
        public Guid? RepresentativeId { get; set; }//müşteri temsilcisi
        public Guid? CurrentAccountId { get; set; }
        public int? SectorId { get; set; } //sektör
        public int? ActivityAreaId { get; set; } //faliyet alanı
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneCountryCode { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? CreatedDate { get; set; }
        public string? PanelPrivileges { get; set; }//bunları keycloak role olarak basıcaz //permissions role
        public Guid? UserGroupRoleId { get; set; }
        public SiteStatusEnum? SiteStatus { get; set; }
        public UserStatusEnum? UserStatus { get; set; }
        public Guid? BusinessId { get; set; }
        public string BusinessCode { get; init; } = null!;
        public ReviewStatusEnum BusinessReviewStatus { get; init; }
        public Guid IdentityRefId { get; set; }
    }
}
