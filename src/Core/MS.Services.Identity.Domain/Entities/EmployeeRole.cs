using MS.Services.Core.Data.Data.Attributes;

namespace MS.Services.Identity.Domain.Entities;
public class EmployeeRole : BaseSoftDeleteEntity
{
    [QuerySearch]
    public string Name { get; set; } = null!;
    [QuerySearch]
    public string Description { get; set; } = null!;
    public int? DiscountRate { get; set; }

    public ICollection<UserEmployee> UserEmployees { get; set; }
}
