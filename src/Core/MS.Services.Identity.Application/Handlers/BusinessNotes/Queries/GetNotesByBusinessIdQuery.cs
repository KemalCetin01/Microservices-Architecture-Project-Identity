using AutoMapper;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;
using MS.Services.Identity.Application.Handlers.Notes.Queries;
using MS.Services.Identity.Domain.Filters.NoteFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MS.Services.Identity.Application.Core.Infrastructure.Services;

namespace MS.Services.Identity.Application.Handlers.BusinessNotes.Queries;

public class GetNotesByBusinessIdQuery : SearchQuery<BusinessNoteQueryFilter, PagedResponse<NoteDTO>>
{
}

public class BusinessNoteQueryFilter : IFilter
{
    public Guid RelationId { get; set; }
}

public sealed class GetNotesByBusinessIdQueryHandler : BaseQueryHandler<GetNotesByBusinessIdQuery, PagedResponse<NoteDTO>>
{
    protected readonly IBusinessNoteService _noteService;
    protected readonly IMapper _mapper;

    public GetNotesByBusinessIdQueryHandler(IBusinessNoteService noteService, IMapper mapper)
    {
        _noteService = noteService;
        _mapper = mapper;
    }

    public override async Task<PagedResponse<NoteDTO>> Handle(GetNotesByBusinessIdQuery request, CancellationToken cancellationToken  )
    {
        var searchResult = _mapper.Map<SearchQueryModel<NoteQueryServiceFilter>>(request);

      return await _noteService.GetAllByBusinessNoteId(request.Filter.RelationId, searchResult, cancellationToken);

    }
}
