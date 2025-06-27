using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Notes.DTOs;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Domain.Filters.NoteFilters;
using System.Text.Json.Serialization;

namespace MS.Services.Identity.Application.Handlers.Notes.Queries;

public class GetNotesByRelationIdQuery : SearchQuery<NoteQueryFilter, PagedResponse<NoteDTO>>
{
    //[JsonIgnore]
    //public NoteTypeEnum NoteType { get; set; }
}

public class NoteQueryFilter : IFilter
{
    public Guid RelationId { get; set; }

}

//public sealed class GetNotesQueryHandler : BaseQueryHandler<GetNotesByRelationIdQuery, PagedResponse<NoteDTO>>
//{
//    protected readonly INoteService _noteService;
//    protected readonly IMapper _mapper;

//    public GetNotesQueryHandler(INoteService noteService, IMapper mapper)
//    {
//        _noteService = noteService;
//        _mapper = mapper;
//    }

//    public override async Task<PagedResponse<NoteDTO>> Handle(GetNotesByRelationIdQuery request, CancellationToken cancellationToken  )
//    {
//        var searchResult = _mapper.Map<SearchQueryModel<NoteQueryServiceFilter>>(request);

//        var result = await _noteService.GetAllByRelationId(request.RelationId, searchResult, cancellationToken);

//        return _mapper.Map<PagedResponse<NoteDTO>>(result);
//    }
//}