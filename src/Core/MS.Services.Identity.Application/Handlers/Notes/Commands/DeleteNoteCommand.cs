using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;

namespace MS.Services.Identity.Application.Handlers.Notes.Commands;

public class DeleteNoteCommand : ICommand<NoteDTO>
{
    public Guid Id { get; set; }
}
public sealed class DeleteNoteCommandHandler : BaseCommandHandler<DeleteNoteCommand, NoteDTO>
{
    private readonly INoteService _noteService;
    public DeleteNoteCommandHandler(INoteService noteService)
    {
        _noteService = noteService;
    }
    public override async Task<NoteDTO> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {

        return await _noteService.Remove(request.Id, cancellationToken);
    }
}
