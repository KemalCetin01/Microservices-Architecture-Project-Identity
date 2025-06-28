using MS.Services.Core.Data.Data.Attributes;
using MS.Services.Core.Data.Data.Entities;
using MS.Services.Identity.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace MS.Services.Identity.Domain.Entities;

public class ActivityArea : BaseSoftDeleteIntIdEntity
{
    [QuerySearch]
    public string Name { get; set; }
    public StatusEnum Status { get; set; }
}
