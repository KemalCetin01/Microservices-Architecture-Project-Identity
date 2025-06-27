using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

public class UserType : BaseEntity<int>
{
    public override int Id { get; init; }
    public string Name { get; set; } = null!;
    public ICollection<User> Users{ get; set; }
}
