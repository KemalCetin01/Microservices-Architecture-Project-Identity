using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Identity.Application.Helpers;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Application.Handlers.B2bRoles.Queries;

public class GetAllBusinessRoleQuery : IQuery<ListResponse<LabelValueResponse>>
{
    public Guid BusinessId { get; set; }
    public string Search { get; set; }
}

public sealed class SearchFKeyBusinessRoleQueryHandler : BaseQueryHandler<GetAllBusinessRoleQuery, ListResponse<LabelValueResponse>>
{

    private readonly IMapper _mapper;
    //private readonly IKeycloakGroupService _keycloakGroupService;
    private readonly IBusinessService _businessService;
    private readonly KeycloakOptions _keycloakOptions;

    public SearchFKeyBusinessRoleQueryHandler(IMapper mapper, IBusinessService businessService,
        IOptions<KeycloakOptions> options)
    {
        _mapper = mapper;
        _businessService = businessService;
        _keycloakOptions = options.Value;
    }

    public override async Task<ListResponse<LabelValueResponse>> Handle(GetAllBusinessRoleQuery request, CancellationToken cancellationToken  )
    {
        throw new NotImplementedException("this function is not implemented yet..");
        /*
        if (request.BusinessId == null)
            throw new ApiException(BusinessesConstants.BusinessIdCanNotBeNull);

        var business = await _businessService.GetByIdAsync(request.BusinessId, cancellationToken);

        //TODO IdentityGrouprefId check yapılacak...

        var result = await _keycloakGroupService.GetAllB2BRolesAsync(request.Search, business.IdentityGroupRefId!.Value, _keycloakOptions.ecommerce_b2b_realm, cancellationToken);
        return new ListResponse<LabelValueResponse>(_mapper.Map<ICollection<LabelValueResponse>>(result));
        */
    }

}