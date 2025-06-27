using MS.Services.Core.Data.Data.Models;

namespace MS.Services.Identity.Domain.EntityFilters;

public class SearchBusinessRolesQueryFilterModel : IFilterModel
{
    public Guid BusinessId { get; set; }
    public string name { get; set; }
}
