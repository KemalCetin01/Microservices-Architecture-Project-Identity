using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;

namespace MS.Services.Identity.Application.Handlers.Notes.Queries;

public class GetNoteByIdQuery : IQuery<NoteDTO>
{
    public Guid Id { get; set; }
}

public sealed class GetNoteByIdQueryHandler : BaseQueryHandler<GetNoteByIdQuery, NoteDTO>
{
    protected readonly INoteService _noteService;

    public GetNoteByIdQueryHandler(INoteService noteService)
    {
        _noteService = noteService;
    }
    public override async Task<NoteDTO> Handle(GetNoteByIdQuery request, CancellationToken cancellationToken  )
    {
        return await _noteService.GetById(request.Id, cancellationToken);
    }
}