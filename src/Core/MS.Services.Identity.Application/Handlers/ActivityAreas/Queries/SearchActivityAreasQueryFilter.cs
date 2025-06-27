using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Handlers.Search;

namespace MS.Services.Identity.Application.Handlers.ActivityAreas.Queries;

public class SearchActivityAreasQueryFilter:IFilter
{
    public string name { get; set; }
    public StatusEnum Status { get; set; }

}
