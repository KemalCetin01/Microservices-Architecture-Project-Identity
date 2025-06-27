using FluentValidation;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Domain.EntityConstants;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Application.Handlers.Business.Commands;
public class CreateBusinessRoleCommand : CreateBusinessRoleRequestDto, ICommand
{

}
public sealed class CreateBusinessRoleCommandHandler : BaseCommandHandler<CreateBusinessRoleCommand>
{
    private readonly IBusinessService _businessService;
    private readonly IIdentityB2BService _identityB2BService;

    public CreateBusinessRoleCommandHandler(IBusinessService businessService, IIdentityB2BService identityB2BService)
    {
        //_keycloakGroupService = keycloakGroupService;
        _businessService = businessService;
        _identityB2BService = identityB2BService;
    }

    public override async Task Handle(CreateBusinessRoleCommand request, CancellationToken cancellationToken)
    {
        var business = await _businessService.GetByIdAsync(request.BusinessId, cancellationToken);
        if (business.IdentityRefId == null) throw new ApiException("Business reference id can not be null");
        await _identityB2BService.CreateBusinessRoleAsync(business.IdentityRefId.Value, request, cancellationToken);
    }
}

public class CreateBusinessRoleCommandValidator : AbstractValidator<CreateBusinessRoleCommand>
{
    public CreateBusinessRoleCommandValidator()
    {
        RuleFor(x => x.BusinessId).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(BusinessConstants.RoleNameMaxLength).NotEmpty();
    }
}