    using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.Notes.DTOs;

public class NoteDTO : IResponse
{
    public Guid? Id { get; set; }
    public string? Content { get; set; }
    public string? Status { get; set; }
    public DateTime? CreatedDate { get; set; }
    public string? CreatedBy { get; set; }
}
