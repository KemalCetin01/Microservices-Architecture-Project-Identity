using MS.Services.Core.Data.Data.Models;

namespace MS.Services.Identity.Domain.Filters.AddressFilters;

public class AddressQueryServiceFilter : IFilterModel
{
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? FullName { get; set; }
    public string? BillingType { get; set; }
    public string? ZipCode { get; set; }
    public string? Email { get; set; }
}
