using FluentValidation;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Business.Request;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Application.Handlers.Business.Commands;

public sealed class UpdateBusinessPermissionsCommand : UpdateBusinessPermissionsRequestDto, ICommand
{

}
public sealed class UpdateBusinessPermissionsCommandHandler : BaseCommandHandler<UpdateBusinessPermissionsCommand>
{
    private readonly IBusinessService _businessService;
    private readonly IIdentityB2BService _identityB2BService;

    public UpdateBusinessPermissionsCommandHandler(IBusinessService businessService, IIdentityB2BService identityB2BService)
    {
        _businessService = businessService;
        _identityB2BService = identityB2BService;
    }


    public override async Task Handle(UpdateBusinessPermissionsCommand request, CancellationToken cancellationToken)
    {    
        var business = await _businessService.GetByIdAsync(request.BusinessId, cancellationToken);
        if (business.IdentityRefId == null) throw new ResourceNotFoundException("Identity server business reference id is null");

        await _identityB2BService.UpdateBusinessPermissionsAsync(business.IdentityRefId!.Value, request.Permissions, cancellationToken);
    }
}


public sealed class UpdateBusinessPermissionsCommandValidator : AbstractValidator<UpdateBusinessPermissionsCommand>
{
    public UpdateBusinessPermissionsCommandValidator()
    {
        RuleFor(x => x.BusinessId).NotEmpty();

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

