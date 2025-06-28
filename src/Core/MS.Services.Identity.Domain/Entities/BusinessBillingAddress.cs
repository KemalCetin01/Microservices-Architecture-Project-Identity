using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

public class BusinessBillingAddress : BaseBillingAddress
{
    public Guid BusinessId { get; set; }
    public Business Business { get; set; } = null!;
   
}
