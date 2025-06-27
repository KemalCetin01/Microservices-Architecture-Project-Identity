using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Handlers.Search;

namespace MS.Services.Identity.Application.Handlers.Sectors.Queries;

public class SearchSectorsQueryFilter:IFilter
{
    public string name { get; set; }
    public StatusEnum Status { get; set; }

}
