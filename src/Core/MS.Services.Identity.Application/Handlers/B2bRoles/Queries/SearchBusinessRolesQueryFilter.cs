using MS.Services.Core.Base.Handlers.Search;

namespace MS.Services.Identity.Application.Handlers.B2bRoles.Queries;

public class SearchBusinessRolesQueryFilter : IFilter
{
    public Guid BusinessId { get; set; }
    public string name { get; set; }
}
