using AutoMapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Positions.DTOs;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;

namespace MS.Services.Identity.Application.Handlers.Positions.Queries;

public class SearchPositionsQuery : SearchQuery<SearchPositionsQueryFilter, PagedResponse<PositionDTO>>
{
}
public sealed class SearchPositionsQueryHandler : BaseQueryHandler<SearchPositionsQuery, PagedResponse<PositionDTO>>
{
    protected readonly IPositionService _positionService;
    protected readonly IMapper _mapper;

    public SearchPositionsQueryHandler(IPositionService positionService, IMapper mapper)
    {
        _positionService = positionService;
        _mapper = mapper;
    }

    public override async Task<PagedResponse<PositionDTO>> Handle(SearchPositionsQuery request, CancellationToken cancellationToken  )
    {
        var searchResult = _mapper.Map<SearchQueryModel<SearchPositionsQueryFilterModel>>(request);

        return await _positionService.SearchAsync(searchResult, cancellationToken);
      
    }

}