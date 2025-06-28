namespace MS.Services.Identity.Domain.Entities
{
    public class UserEmployee : ISoftDeleteEntity,IEntity
    {
        public string? PhoneNumber { get; set; }
        public DateTime? LastDateEntry { get; set; }

        public Guid? EmployeeRoleId { get; set; }
        public EmployeeRole? EmployeeRole { get; set; } = null!;
        public ICollection<UserEmployeeManager>? EmployeeManagers { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }

        public Guid? DeletedBy { get; set; }
    }
}
