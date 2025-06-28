using MS.Services.Core.Data.Data.Models;

namespace MS.Services.Identity.Domain.EntityFilters;
public class SearchUserEmployeesQueryFilterModel : IFilterModel
{
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Role { get; set; } = null!;
    public DateTime? firstRangeLastEntryDate { get; set; }
    public DateTime? lastRangeLastEntryDate { get; set; }
    public int FirstDiscountRate { get; set; }
    public int LastDiscountRate { get; set; }
}
