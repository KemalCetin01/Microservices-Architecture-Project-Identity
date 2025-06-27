using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.BusinessNotes.Commands;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Application.Handlers.CurrentAccountNotes.Commands;

public class CreateCurrentAccountNoteCommand : ICommand<NoteDTO>
{
    public Guid RelationId { get; set; }
    public string? Content { get; set; }
    public string? Status { get; set; }
}

public sealed class CreateCurrentAccountNoteCommandHandler : BaseCommandHandler<CreateCurrentAccountNoteCommand, NoteDTO>
{
    private readonly ICurrentAccountNoteService currentAccountNoteService;
    public CreateCurrentAccountNoteCommandHandler(ICurrentAccountNoteService noteService)
    {
        currentAccountNoteService = noteService;
    }
    public override async Task<NoteDTO> Handle(CreateCurrentAccountNoteCommand request, CancellationToken cancellationToken)
    {
        return await currentAccountNoteService.AddAsync(request, cancellationToken);
    }
}
