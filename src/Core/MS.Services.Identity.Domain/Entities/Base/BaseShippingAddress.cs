namespace MS.Services.Identity.Domain.Entities;

public abstract class BaseShippingAddress : BaseAddress
{
    /// <summary>
    ///     Name of the address which is entered by customer.
    /// </summary>
    public string? Name { get; set; }
    public string? DeliveryContactName { get; set; }

}