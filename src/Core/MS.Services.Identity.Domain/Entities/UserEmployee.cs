namespace MS.Services.Identity.Domain.Entities
{
    public class UserEmployee : ISoftDeleteEntity,IEntity
    {
        public string? PhoneNumber { get; set; }
        public DateTime? LastDateEntry { get; set; }

        public Guid? EmployeeRoleId { get; set; }
        public EmployeeRole? EmployeeRole { get; set; } = null!;
        public ICollection<UserEmployeeManager>? EmployeeManagers { get; set; }
        public ICollection<UserB2C>? UserB2Cs { get; set; }
        public ICollection<UserB2B>? UserB2Bs { get; set; }

        public ICollection<Business>? Businesses { get; set; }
        //   public ICollection<User>? Users { get; set; } // müşteri temsilcisi ?
        public Guid UserId { get; set; }
        public User User { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime? DeletedDate { get; set; }

        public Guid? DeletedBy { get; set; }
    }
}
