using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;

namespace MS.Services.Identity.Application.Handlers.UserNotes.Commands;

public class CreateUserNoteCommand : ICommand<NoteDTO>
{
    public Guid RelationId { get; set; }
    public string? Content { get; set; }
    public string? Status { get; set; }
}

public sealed class CreateUserNoteCommandHandler : BaseCommandHandler<CreateUserNoteCommand, NoteDTO>
{
    private readonly IUserNoteService _noteService;
    public CreateUserNoteCommandHandler(IUserNoteService noteService)
    {
        _noteService = noteService;
    }
    public override async Task<NoteDTO> Handle(CreateUserNoteCommand request, CancellationToken cancellationToken)
    {
        return await _noteService.AddAsync(request, cancellationToken);
    }
}
