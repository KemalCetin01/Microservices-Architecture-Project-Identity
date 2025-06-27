using MS.Services.Core.Data.Data.Attributes;
//using MS.Services.Core.Localization.Attributes;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

public class Sector: BaseSoftDeleteIntIdEntity
{
    [QuerySearch]
    //[Localize]
    public string Name { get; set; }
    public StatusEnum Status { get; set; }
}