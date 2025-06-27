namespace MS.Services.Identity.Domain.Entities;

public class DocumentRelation : BaseAuditableGuidEntity
{
    #region Navigation Properties

    public Document Document { get; set; } = null!;

    #endregion

    #region Scalar Properties

    public Guid DocumentId { get; set; }
    public string Entity { get; set; } = null!;
    public string EntityId { get; set; } = null!;
    public string EntityField { get; set; } = null!;
    public short Order { get; set; }

    #endregion
}