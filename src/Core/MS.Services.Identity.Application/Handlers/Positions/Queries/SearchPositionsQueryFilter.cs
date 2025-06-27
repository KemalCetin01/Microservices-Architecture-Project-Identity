using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Handlers.Search;

namespace MS.Services.Identity.Application.Handlers.Positions.Queries;

public class SearchPositionsQueryFilter:IFilter
{
    public string name { get; set; }
    public StatusEnum Status { get; set; }

}
