using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Helpers;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2CUser.Register;

public class B2CChangePasswordCommand : ChangePasswordCommandDTO, ICommand<DataResponse<bool>>
{
}
public sealed class B2CChangePasswordCommandHandler : BaseCommandHandler<B2CChangePasswordCommand, DataResponse<bool>>
{
    private readonly IAuthB2CService _authenticationService;
    private readonly KeycloakOptions _keycloakOptions;

    public B2CChangePasswordCommandHandler(IAuthB2CService authenticationService,
                                 IOptions<KeycloakOptions> options)
    {
        _authenticationService = authenticationService;
        _keycloakOptions = options.Value;
    }


    public override async Task<DataResponse<bool>> Handle(B2CChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var result= await _authenticationService.ChangePasswordAsync(request, _keycloakOptions.ecommerce_b2c_realm, cancellationToken);
        return new DataResponse<bool> {Data= result };
    }
}

