using MS.Services.Core.Data.Data.Attributes;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

public class NumberOfEmployee : BaseSoftDeleteIntIdEntity
{
    [QuerySearch]
    public string Name { get; set; }
    public StatusEnum Status { get; set; }
}
