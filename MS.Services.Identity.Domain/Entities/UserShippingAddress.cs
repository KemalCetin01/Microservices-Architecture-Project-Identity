namespace MS.Services.Identity.Domain.Entities;

public class UserShippingAddress : BaseShippingAddress
{
    public Guid UserId { get; set; }
    public User? User { get; set; }
}
