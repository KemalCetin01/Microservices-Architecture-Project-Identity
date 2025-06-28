using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Helpers;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Application.Handlers.B2bRoles.Commands;
public class CreateRoleChildGroupCommand : ICommand<DataResponse<bool>>
{
    public string Name { get; set; }
    public Guid BusinessId { get; set; }
}
public sealed class CreateRoleChildGroupCommandHandler : BaseCommandHandler<CreateRoleChildGroupCommand, DataResponse<bool>>
{
    private readonly KeycloakOptions _keycloakOptions;

    public CreateRoleChildGroupCommandHandler(
        IOptions<KeycloakOptions> options)
    {
        _keycloakOptions = options.Value;
    }

    public override async Task<DataResponse<bool>> Handle(CreateRoleChildGroupCommand request, CancellationToken cancellationToken)
    {
        if (request.BusinessId == null)
            throw new ApiException(BusinessesConstants.BusinessIdCanNotBeNull);
        throw new NotImplementedException("this function is not implemented yet..");
        /*
        var business = await _businessService.GetByIdAsync(request.BusinessId, cancellationToken);

        var roleGroupModel = new KeycloakBusinessGroupModel() { name = request.Name };
        var result = await _keycloakGroupService.CreateChildGroupAsync(_keycloakOptions.ecommerce_b2b_realm, business.IdentityGroupRefId.ToString(), roleGroupModel, cancellationToken);
        return new DataResponse<bool> { Data = result.Data };
        */
    }
}