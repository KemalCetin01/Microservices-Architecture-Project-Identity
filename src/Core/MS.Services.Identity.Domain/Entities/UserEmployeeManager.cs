namespace MS.Services.Identity.Domain.Entities
{
    public class UserEmployeeManager : BaseSoftDeleteEntity
    {
        public Guid EmployeeId { get; set; }
        public UserEmployee UserEmployee { get; set; }
        public Guid ManagerId { get; set; }
        public UserEmployee Manager { get; set; }
    }
}
