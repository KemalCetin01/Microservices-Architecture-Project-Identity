using Microsoft.AspNetCore.Http;
using MS.Services.Core.Base.Enums;

namespace MS.Services.Identity.Application.Handlers.Files.DTOs.Request;

public class FileUploadRequestDto
{
    public IFormFile File { get; set; } = null!;
    public AWSAccessControlType AccessControlType { get; set; }
    public FileUploadSubRelationRequestDto? Relation { get; set; }
}

public class FileUploadSubRelationRequestDto
{
    public string Entity { get; set; } = null!;
    public string EntityId { get; set; } = null!;
    public string EntityField { get; set; } = null!;
    public short Order { get; set; } = 1;
}