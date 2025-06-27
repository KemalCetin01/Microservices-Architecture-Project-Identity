using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.User.DTOs;

public class B2BUserListDTO
{
    public Guid Id { get; init; }
    public Guid BusinessId { get; init; }
    public string BusinessCode { get; init; }
    public ReviewStatusEnum BusinessReviewStatus { get; init; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
    public string? BusinessName { get; set; }
    public bool? BusinessStatus { get; set; }
    public SiteStatusEnum SiteStatus { get; set; }// status dediğimiz olay bu kullanıcı biri tarafından mı oluşturulmuş yoksa kendi mi oluşmuş
    public string? Representative { get; set; } //müşteri temsilcisiymiş // UserEmployee
    public string? LastLoginDate { get; set; } // son giriş yazıyordu mockupta
}