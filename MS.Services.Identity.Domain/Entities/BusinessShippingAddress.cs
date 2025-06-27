
namespace MS.Services.Identity.Domain.Entities;
public class BusinessShippingAddress : BaseShippingAddress
{
    public Guid BusinessId { get; set; }
    public Business? Business { get; set; }
}
