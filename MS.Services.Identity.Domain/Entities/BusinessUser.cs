namespace MS.Services.Identity.Domain.Entities
{
    public class BusinessUser : BaseGuidEntity
    {
        public Guid BusinessId { get; set; }
        public Business Business { get; set; } = null!;
        public Guid UserId { get; set; }
        public UserB2B UserB2B { get; set; } = null!;

    }
}
