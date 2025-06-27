using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.B2bRoles.DTOs;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Domain.EntityFilters;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Application.Handlers.B2bRoles.Queries;

public class SearchBusinessRolesQuery : SearchQuery<SearchBusinessRolesQueryFilter, PagedResponse<B2bGroupRoleDTO>>
{
}
public sealed class SearchBusinessRolesQueryHandler : BaseQueryHandler<SearchBusinessRolesQuery, PagedResponse<B2bGroupRoleDTO>>
{
    private readonly IMapper _mapper;
    //private readonly IKeycloakGroupService _keycloakGroupService;
    private readonly IBusinessService _businessService;
    private readonly KeycloakOptions _keycloakOptions;
    public SearchBusinessRolesQueryHandler(IMapper mapper, IBusinessService businessService,
        IOptions<KeycloakOptions> options)
    {
        _mapper = mapper;
        _businessService = businessService;
        _keycloakOptions = options.Value;
    }

    public override async Task<PagedResponse<B2bGroupRoleDTO>> Handle(SearchBusinessRolesQuery request, CancellationToken cancellationToken  )
    {
        throw new NotImplementedException("this function is not implemented yet..");
        /*
        if (request.Filter.BusinessId == null)
            throw new ApiException(BusinessesConstants.BusinessIdCanNotBeNull);

        var business = await _businessService.GetByIdAsync(request.Filter.BusinessId, cancellationToken);

        var searchResult = _mapper.Map<SearchQueryModel<SearchBusinessRolesQueryFilterModel>>(request);

        var result = await _keycloakGroupService.SearchAsync(searchResult, business.IdentityGroupRefId.Value, _keycloakOptions.ecommerce_b2b_realm, cancellationToken);

        return result;
        */
    }
}
