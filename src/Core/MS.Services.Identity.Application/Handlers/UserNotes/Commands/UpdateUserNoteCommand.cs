using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.CurrentAccountNotes.Commands;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Application.Handlers.UserNotes.Commands;

public class UpdateUserNoteCommand : ICommand<NoteDTO>
{
    public Guid Id { get; set; }
    public Guid RelationId { get; set; }
    public string? Content { get; set; }
    public string? Status { get; set; }
}

public sealed class UpdateUserNoteCommandHandler : BaseCommandHandler<UpdateUserNoteCommand, NoteDTO>
{
    private readonly IUserNoteService _noteService;
    public UpdateUserNoteCommandHandler(IUserNoteService noteService)
    {
        _noteService = noteService;
    }
    public override async Task<NoteDTO> Handle(UpdateUserNoteCommand request, CancellationToken cancellationToken)
    {
        return await _noteService.UpdateAsync(request, cancellationToken);
    }
}
