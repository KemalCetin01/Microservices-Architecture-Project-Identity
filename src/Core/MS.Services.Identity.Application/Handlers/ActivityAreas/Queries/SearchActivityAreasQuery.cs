using AutoMapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.ActivityAreas.DTOs;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;

namespace MS.Services.Identity.Application.Handlers.ActivityAreas.Queries;

public class SearchActivityAreasQuery : SearchQuery<SearchActivityAreasQueryFilter, PagedResponse<ActivityAreaDTO>>
{
}
public sealed class SearchActivityAreasQueryHandler : BaseQueryHandler<SearchActivityAreasQuery, PagedResponse<ActivityAreaDTO>>
{
    protected readonly IActivityAreaService _activityAreaService;
    protected readonly IMapper _mapper;

    public SearchActivityAreasQueryHandler(IActivityAreaService activityAreaService, IMapper mapper)
    {
        _activityAreaService = activityAreaService;
        _mapper = mapper;
    }

    public override async Task<PagedResponse<ActivityAreaDTO>> Handle(SearchActivityAreasQuery request, CancellationToken cancellationToken  )
    {
        var searchResult = _mapper.Map<SearchQueryModel<SearchActivityAreasQueryFilterModel>>(request);

        return await _activityAreaService.SearchAsync(searchResult, cancellationToken);
      
    }

}