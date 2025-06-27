using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Identity.Response;
using MS.Services.Identity.Application.Handlers.User.DTOs;
using MS.Services.Identity.Application.Helpers;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Application.Handlers.User.Queries.B2B;

public class GetB2BUserPermissionsQuery : IQuery<GetB2BUserRoleAndPermissionsResponseDto>
{
    public Guid Id { get; set; }
}
public sealed class GetB2BUserPermissionsQueryueryHandler : BaseQueryHandler<GetB2BUserPermissionsQuery, GetB2BUserRoleAndPermissionsResponseDto>
{
    protected readonly IIdentityB2BService _identityB2BService;
    protected readonly IUserB2BService _userB2BService;
    private readonly KeycloakOptions _keycloakOptions;

    public GetB2BUserPermissionsQueryueryHandler(IUserB2BService userB2BService, IIdentityB2BService identityB2BService,
        IOptions<KeycloakOptions> options)
    {
        _userB2BService = userB2BService;
        _identityB2BService = identityB2BService;
        _keycloakOptions = options.Value;
    }

    public override async Task<GetB2BUserRoleAndPermissionsResponseDto> Handle(GetB2BUserPermissionsQuery request, CancellationToken cancellationToken  )
    {
        var userDetail = await _userB2BService.GetByIdAsync(request.Id, cancellationToken);
        if (userDetail == null)
            throw new ResourceNotFoundException(B2BUserConstants.RecordNotFound);
      
        var response = await _identityB2BService.GetB2BUserRoleAndPermissions(userDetail.IdentityRefId, cancellationToken);
        return response;
    }
}