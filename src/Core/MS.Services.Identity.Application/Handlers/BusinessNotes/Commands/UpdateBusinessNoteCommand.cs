using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Notes.Commands;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Application.Handlers.BusinessNotes.Commands;

public class UpdateBusinessNoteCommand : ICommand<NoteDTO>
{
    public Guid Id { get; set; }
    public Guid RelationId { get; set; }
    public string? Content { get; set; }
    public string? Status { get; set; }
}

public sealed class UpdateBusinesNoteCommandHandler : BaseCommandHandler<UpdateBusinessNoteCommand, NoteDTO>
{
    private readonly IBusinessNoteService _noteService;
    public UpdateBusinesNoteCommandHandler(IBusinessNoteService noteService)
    {
        _noteService = noteService;
    }
    public override async Task<NoteDTO> Handle(UpdateBusinessNoteCommand request, CancellationToken cancellationToken)
    {
        return await _noteService.UpdateAsync(request, cancellationToken);
    }
}
