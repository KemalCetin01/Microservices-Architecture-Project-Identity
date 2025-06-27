using MS.Services.Core.Data.Data.Models;

namespace MS.Services.Identity.Domain.Filters.NoteFilters;

public class NoteQueryServiceFilter : IFilterModel
{
    string? Content { get; set; }
    string? Status { get; set; }
}
