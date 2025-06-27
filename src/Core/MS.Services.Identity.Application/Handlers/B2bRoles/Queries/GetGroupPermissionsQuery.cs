using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.B2bRoles.DTOs;
using MS.Services.Identity.Application.Helpers;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Application.Handlers.B2bRoles.Queries;

public class GetGroupPermissionsQuery : IQuery<ListResponse<B2BRolePermissionsDTO>>
{
    public Guid BusinessId { get; set; }
    public Guid ChildGroupId { get; set; }
}
public sealed class GetGroupPermissionsCommandQueryHandler : BaseQueryHandler<GetGroupPermissionsQuery, ListResponse<B2BRolePermissionsDTO>>
{
    //protected readonly IKeycloakRoleService _keycloakRoleService;
    private readonly IMapper _mapper;
    private readonly KeycloakOptions _keycloakOptions;
    private readonly IBusinessService _businessService;

    public GetGroupPermissionsCommandQueryHandler(IMapper mapper, IBusinessService businessService,
        IOptions<KeycloakOptions> options)
    {
        _mapper = mapper;
        _keycloakOptions = options.Value;
        _businessService = businessService;
    }

    public override async Task<ListResponse<B2BRolePermissionsDTO>> Handle(GetGroupPermissionsQuery request, CancellationToken cancellationToken  )
    {
        throw new NotImplementedException("this function is not implemented yet..");
        /*
        if(request.BusinessId==null)
            throw new ApiException(BusinessesConstants.BusinessIdCanNotBeNull);
        var business = await _businessService.GetByIdAsync(request.BusinessId, cancellationToken);

        var result = await _keycloakRoleService.GetChildGroupRolePermissions(business.IdentityGroupRefId.ToString(), request.ChildGroupId.ToString(), _keycloakOptions.ecommerce_b2b_realm, KeycloakConstants.ecommerceClient, cancellationToken);
        return new ListResponse<B2BRolePermissionsDTO>(_mapper.Map<ICollection<B2BRolePermissionsDTO>>(result));
        */
    }
}