using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;
using MS.Services.Identity.Application.Helpers;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2BUser.Register;

public class B2BChangePasswordCommand : ChangePasswordCommandDTO, ICommand<DataResponse<bool>>
{
}
public sealed class B2BChangePasswordCommandHandler : BaseCommandHandler<B2BChangePasswordCommand, DataResponse<bool>>
{
    private readonly IAuthB2BService _authenticationService;
    private readonly KeycloakOptions _keycloakOptions;

    public B2BChangePasswordCommandHandler(IAuthB2BService authenticationService,
                                 IOptions<KeycloakOptions> options)
    {
        _authenticationService = authenticationService;
        _keycloakOptions = options.Value;
    }


    public override async Task<DataResponse<bool>> Handle(B2BChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var result= await _authenticationService.ChangePasswordAsync(request, _keycloakOptions.ecommerce_b2b_realm, cancellationToken);
        return new DataResponse<bool> {Data= result };
    }
}

