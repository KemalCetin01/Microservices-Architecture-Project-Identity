using MS.Services.Core.Data.Data.Models;

namespace MS.Services.Identity.Domain.EntityFilters;

public class BusinessCurrentAccountSearchFilter : IFilterModel
{
    public Guid BusinessId { get; init; }
    public string? ErpRefId { get; set; }
    public string? Code { get; set; }
    public string? CurrentAccountName { get; set; }
}
