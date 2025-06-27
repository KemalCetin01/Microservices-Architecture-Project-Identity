using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Data.Data.Models;

namespace MS.Services.Identity.Domain.EntityFilters;

public class SearchPositionsQueryFilterModel : IFilterModel
{
    public string name { get; set; }
    public StatusEnum Status { get; set; }
}
