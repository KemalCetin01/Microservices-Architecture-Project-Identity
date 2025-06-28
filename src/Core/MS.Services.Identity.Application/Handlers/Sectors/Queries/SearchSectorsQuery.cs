using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Sectors.DTOs;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Handlers.Sectors.Queries;

public class SearchSectorsQuery : SearchQuery<SearchSectorsQueryFilter, PagedResponse<SectorDTO>>
{
}
public sealed class SearchSectorsQueryHandler : BaseQueryHandler<SearchSectorsQuery, PagedResponse<SectorDTO>>
{
    protected readonly ISectorService _sectorService;
    protected readonly IMapper _mapper;


    public SearchSectorsQueryHandler(ISectorService sectorService, IMapper mapper)
    {
        _sectorService = sectorService; 
        _mapper = mapper;
    }

    public override async Task<PagedResponse<SectorDTO>> Handle(SearchSectorsQuery request, CancellationToken cancellationToken  )
    {
        var searchResult = _mapper.Map<SearchQueryModel<SearchSectorsQueryFilterModel>>(request);
        return await _sectorService.SearchAsync(searchResult, cancellationToken);
      
    }

}