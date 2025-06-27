using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.Business.Queries;

public sealed class SearchBusinessesQuery : SearchQuery<SearchBusinessesQueryFilter, PagedResponse<SearchBusinessResponseDto>>
{

}

public sealed class SearchBusinessesQueryHandler : BaseQueryHandler<SearchBusinessesQuery, PagedResponse<SearchBusinessResponseDto>>
{
    private readonly IBusinessService _businessService;
    private readonly IMapper _mapper;

    public SearchBusinessesQueryHandler(IBusinessService businessService, IMapper mapper)
    {
        _businessService = businessService;
        _mapper = mapper;
    }

    public override async Task<PagedResponse<SearchBusinessResponseDto>> Handle(SearchBusinessesQuery request, CancellationToken cancellationToken  )
    {
        var searchQueryModel = _mapper.Map<SearchQueryModel<BusinessSearchFilter>>(request);

        return await _businessService.SearchAsync(searchQueryModel, cancellationToken);
    }
}

public sealed class SearchBusinessesQueryFilter : IFilter
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? CountryCode { get; set; }
    public List<int>? CityIds { get; set; }
    public List<int>? TownIds { get; set; }
    public string? BusinessType { get; set; }
    public List<Guid>? RepresentativeIds { get; set; }
    public BusinessStatusEnum? BusinessStatus { get; set; }
    public ReviewStatusEnum? ReviewStatus { get; set; }
    public int? DiscountRate { get; set; }
}
