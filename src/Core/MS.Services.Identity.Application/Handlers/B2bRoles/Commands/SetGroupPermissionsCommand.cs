using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Handlers.B2bRoles.DTOs;
using MS.Services.Identity.Application.Helpers;

namespace MS.Services.Identity.Application.Handlers.B2bRoles.Commands;

public class SetChildGroupPermissionsCommand : SetGroupPermissionsCommandDTO, ICommand
{

}
public sealed class SetChildGroupPermissionsCommandHandler : BaseCommandHandler<SetChildGroupPermissionsCommand>
{
    //private readonly IKeycloakRoleService _keycloakRoleService;
    private readonly KeycloakOptions _keycloakOptions;

    public SetChildGroupPermissionsCommandHandler(
        IOptions<KeycloakOptions> options)
    {
        _keycloakOptions = options.Value;
    }


    public override async Task Handle(SetChildGroupPermissionsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException("this function is not implemented yet..");
        /*
        await _keycloakRoleService.SetGroupRolePermissions(request, _keycloakOptions.ecommerce_b2b_realm,cancellationToken);
        */
    }
}


