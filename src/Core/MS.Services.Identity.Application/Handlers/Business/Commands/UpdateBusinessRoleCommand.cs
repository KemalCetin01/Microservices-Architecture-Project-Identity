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

namespace MS.Services.Identity.Application.Handlers.Business.Commands;

public class UpdateBusinessRoleCommand : UpdateBusinessRoleRequestDto, ICommand
{
}
public sealed class UpdateBusinessRoleCommandHandler : BaseCommandHandler<UpdateBusinessRoleCommand>
{
    private readonly IBusinessService _businessService;
    private readonly IIdentityB2BService _identityB2BService;

    public UpdateBusinessRoleCommandHandler(IBusinessService businessService, IIdentityB2BService identityB2BService)
    {
        _businessService = businessService;
        _identityB2BService = identityB2BService;
    }

    public override async Task Handle(UpdateBusinessRoleCommand request, CancellationToken cancellationToken)
    {
        await _identityB2BService.UpdateBusinessRoleAsync(request, cancellationToken);
    }
}

public class UpdateBusinessRoleCommandValidator : AbstractValidator<UpdateBusinessRoleCommand>
{
    public UpdateBusinessRoleCommandValidator()
    {
        RuleFor(x => x.RoleRefId).NotEmpty();
        RuleFor(x => x.Name).MaximumLength(BusinessConstants.RoleNameMaxLength).NotEmpty();
    }
}