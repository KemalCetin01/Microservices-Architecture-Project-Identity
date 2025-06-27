using AutoMapper;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.CurrentAccountNotes.Queries;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;
using MS.Services.Identity.Domain.Filters.NoteFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Services.Identity.Application.Core.Infrastructure.Services;

namespace MS.Services.Identity.Application.Handlers.UserNotes.Queries;

public class GetNotesByUserNoteQuery : SearchQuery<UserNoteQueryFilter, PagedResponse<NoteDTO>>
{
}

public class UserNoteQueryFilter : IFilter
{
    public Guid RelationId { get; set; }
}

public sealed class GetNotesByUserNoteQueryHandler : BaseQueryHandler<GetNotesByUserNoteQuery, PagedResponse<NoteDTO>>
{
    protected readonly IUserNoteService _noteService;
    protected readonly IMapper _mapper;

    public GetNotesByUserNoteQueryHandler(IUserNoteService noteService, IMapper mapper)
    {
        _noteService = noteService;
        _mapper = mapper;
    }

    public override async Task<PagedResponse<NoteDTO>> Handle(GetNotesByUserNoteQuery request, CancellationToken cancellationToken  )
    {
        var searchResult = _mapper.Map<SearchQueryModel<NoteQueryServiceFilter>>(request);

       return await _noteService.GetAllByUserNoteId(request.Filter.RelationId, searchResult, cancellationToken);

    }
}
