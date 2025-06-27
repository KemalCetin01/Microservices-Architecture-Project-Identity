using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.User.DTOs
{
    public class B2CUserListDTO
    {
        public Guid Id { get; init; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? CreatedDate { get; set; }
        public int? CountryId { get; set; }
        public int? CityId { get; set; }
        public int? TownId { get; set; }
        public UserProfileEnum? UserProfile { get; set; }
        public int? UserType { get; set; } //TODO enum a çevrilecek
        public string? Representative { get; set; } // müşteri temsilcisi // UserEmployee
        public SiteStatusEnum SiteStatus { get; set; } // status dediğimiz olay bu kullanıcı biri tarafından mı oluşturulmuş yoksa kendi mi oluşmuş
        public string? Notes { get; set; }
        public string? LastLoginDate { get; set; }
        public int BillingCount { get; set; }
    }
}
