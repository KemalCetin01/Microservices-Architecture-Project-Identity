using AutoMapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Occupations.DTOs;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;

namespace MS.Services.Identity.Application.Handlers.Occupations.Queries;

public class SearchOccupationsQuery : SearchQuery<SearchOccupationsQueryFilter, PagedResponse<OccupationDTO>>
{
}
public sealed class SearchOccupationsQueryHandler : BaseQueryHandler<SearchOccupationsQuery, PagedResponse<OccupationDTO>>
{
    protected readonly IOccupationService _occupationService;
    protected readonly IMapper _mapper;

    public SearchOccupationsQueryHandler(IOccupationService occupationService, IMapper mapper)
    {
        _occupationService = occupationService;
        _mapper = mapper;
    }

    public override async Task<PagedResponse<OccupationDTO>> Handle(SearchOccupationsQuery request, CancellationToken cancellationToken  )
    {
        var searchResult = _mapper.Map<SearchQueryModel<SearchOccupationsQueryFilterModel>>(request);

        return await _occupationService.SearchAsync(searchResult, cancellationToken);
      
    }

}