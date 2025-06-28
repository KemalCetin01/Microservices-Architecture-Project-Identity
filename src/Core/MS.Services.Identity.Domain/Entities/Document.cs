namespace MS.Services.Identity.Domain.Entities;

public class Document : BaseAuditableGuidEntity
{
    #region Navigation Properties

    public ICollection<DocumentRelation>? DocumentRelations { get; set; }

    #endregion

    #region Scalar Properties

    public string Path { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string UploadName { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public bool IsPrivate { get; set; }

    #endregion
}