using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;
using MS.Services.Identity.Domain.Enums;
using System.Text.Json.Serialization;

namespace MS.Services.Identity.Application.Handlers.Notes.Commands;

public class CreateNoteCommand : ICommand<NoteDTO>
{
    public Guid RelationId { get; set; }
    public string? Content { get; set; }
    public string? Status { get; set; }
}

//public sealed class CreateNoteCommandHandler : BaseCommandHandler<CreateNoteCommand, NoteDTO>
//{
//    private readonly INoteService _noteService;
//    public CreateNoteCommandHandler(INoteService noteService)
//    {
//        _noteService = noteService;
//    }
//    public override async Task<NoteDTO> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
//    {
//        return await _noteService.AddAsync(request, cancellationToken);
//    }
//}
