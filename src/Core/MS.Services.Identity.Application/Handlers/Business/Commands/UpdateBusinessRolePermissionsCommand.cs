using FluentValidation;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.DTOs.Business.Response;

namespace MS.Services.Identity.Application.Handlers.Business.Commands;

public class UpdateBusinessRolePermissionsCommand : UpdateBusinessRolePermissionsRequestDto, ICommand
{

}
public sealed class UpdateBusinessRolePermissionsCommandHandler : BaseCommandHandler<UpdateBusinessRolePermissionsCommand>
{
    private readonly IIdentityB2BService _identityB2BService;
    private readonly IBusinessService _businessService;

    public UpdateBusinessRolePermissionsCommandHandler(IIdentityB2BService identityB2BService, IBusinessService businessService)
    {
        _identityB2BService = identityB2BService;
        _businessService = businessService;
    }


    public override async Task Handle(UpdateBusinessRolePermissionsCommand request, CancellationToken cancellationToken)
    {
        GetBusinessResponseDto business = await _businessService.GetByIdAsync(request.BusinessId, cancellationToken);
        if (business.IdentityRefId == null) throw new ApiException("Business identity reference id cannot be null");
        await _identityB2BService.UpdateBusinessRolePermissionsAsync(business.IdentityRefId.Value, request, cancellationToken);
    }
}

public sealed class UpdateBusinessRolePermissionsCommandValidator : AbstractValidator<UpdateBusinessRolePermissionsCommand>
{
    public UpdateBusinessRolePermissionsCommandValidator()
    {
        RuleFor(x => x.BusinessId).NotEmpty();
        RuleFor(x => x.RoleRefId).NotEmpty();

        RuleFor(x => x.Permissions)
            .NotNull()
            .NotEmpty()
            .DependentRules(() =>
            {
                RuleForEach(x => x.Permissions).ChildRules(x => x.RuleFor(p => p.Name).NotEmpty());
                RuleForEach(x => x.Permissions).ChildRules(x => x.RuleFor(p => p.IdentityRefId).NotEmpty());
                RuleForEach(x => x.Permissions).ChildRules(x => x.RuleFor(p => p.Selected).NotNull());
            });
    }

}
