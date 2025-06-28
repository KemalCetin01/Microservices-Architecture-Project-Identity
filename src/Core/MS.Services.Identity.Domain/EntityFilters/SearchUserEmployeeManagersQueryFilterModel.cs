using MS.Services.Core.Data.Data.Models;

namespace MS.Services.Identity.Domain.EntityFilters;
public class SearchUserEmployeeManagersQueryFilterModel : IFilterModel
{
    public Guid Id { get; init; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
}
