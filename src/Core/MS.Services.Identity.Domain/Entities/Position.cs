using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Data.Data.Attributes;

namespace MS.Services.Identity.Domain.Entities;

public class Position : BaseSoftDeleteIntIdEntity
{
    [QuerySearch]
    public string Name { get; set; }
    public StatusEnum Status { get; set; }
}
