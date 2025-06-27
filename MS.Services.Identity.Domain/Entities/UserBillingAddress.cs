using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

public class UserBillingAddress : BaseBillingAddress
{
    /// <summary>
    ///     Name of the address which is entered by customer.
    /// </summary>
    public string? Name { get; set; }
    public Guid UserId { get; set; }
    public User? User { get; set; }
}
