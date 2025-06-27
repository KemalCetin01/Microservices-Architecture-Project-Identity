using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

namespace MS.Services.Identity.Application.Handlers.Auth.Queries;

public class GetUserInfoQuery : IQuery<IdentityUserInfoResponseDto>
{
    public string Token { get; set; }
}
public sealed class GetUserInfoQueryHandler : BaseQueryHandler<GetUserInfoQuery, IdentityUserInfoResponseDto>
{
    private readonly IAuthTokenService _authTokenService;
    public GetUserInfoQueryHandler(IAuthTokenService authTokenService)
    {
        _authTokenService = authTokenService;
    }

    public override async Task<IdentityUserInfoResponseDto> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
    {
        return _authTokenService.GetUserDetailsFromJwtToken(request.Token, cancellationToken);
    }
}