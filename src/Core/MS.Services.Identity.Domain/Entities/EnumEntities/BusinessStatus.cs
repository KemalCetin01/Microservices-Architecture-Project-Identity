using MS.Services.Core.Data.Data.Entities;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Domain.Entities;

/// <summary>
///     Represents status codes of business in the database, mirroring of <see cref="BusinessStatusEnum"/>.
/// </summary>
public sealed class BusinessStatus : BaseSoftDeleteIntIdEntity
{
    #region Scalar Properties

    /// <summary>
    ///     Enum value as string
    /// </summary>
    public string Name { get; set; } = null!;

    #endregion
}