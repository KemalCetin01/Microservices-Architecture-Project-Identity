using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.EntityFilters;

public class BusinessUserSearchFilter : IFilterModel
{
    public Guid BusinessId { get; init; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
}
