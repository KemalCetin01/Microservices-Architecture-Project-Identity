using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

public abstract class BaseAddress : BaseSoftDeleteEntity
{
    public string? PhoneNumber { get; set; }
    public Guid AddressLocationId { get; set; }
    public AddressLocation AddressLocation { get; set; } = null!;
}
