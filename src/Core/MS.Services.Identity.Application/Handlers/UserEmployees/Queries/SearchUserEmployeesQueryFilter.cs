using MS.Services.Core.Base.Handlers.Search;

namespace MS.Services.Identity.Application.Handlers.UserEmployees.Queries;
public class SearchUserEmployeesQueryFilter : IFilter
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

