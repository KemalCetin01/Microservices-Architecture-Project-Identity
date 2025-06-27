namespace MS.Services.Identity.Domain.Entities;

public class ConfirmRegisterType : BaseEntity<int>
{
    public override int Id { get; init; }
    public string Name { get; set; }

}
