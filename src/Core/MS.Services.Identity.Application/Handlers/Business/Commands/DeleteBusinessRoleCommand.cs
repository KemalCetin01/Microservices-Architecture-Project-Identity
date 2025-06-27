using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Domain.Exceptions;

namespace MS.Services.Identity.Application.Handlers.Business.Commands;

public class DeleteBusinessRoleCommand : ICommand
{
    public Guid BusinessRoleRefId { get; set; }
}
public sealed class DeleteBusinessRoleCommandHandler : BaseCommandHandler<DeleteBusinessRoleCommand>
{

    private readonly IIdentityB2BService _identityB2BService;
    private readonly IUserB2BService _userB2BService;

    public DeleteBusinessRoleCommandHandler(IUserB2BService userB2BService, IIdentityB2BService identityB2BService)
    {
        _userB2BService = userB2BService;
        _identityB2BService = identityB2BService;
    }

    public override async Task Handle(DeleteBusinessRoleCommand request, CancellationToken cancellationToken)
    {
        var activeUserInRoleCount = await _userB2BService.GetActiveUserInRoleCountAsync(request.BusinessRoleRefId, cancellationToken);
        if (activeUserInRoleCount > 0)
        {
            var errorMessage = string.Format(UserStatusCodes.ActiveUserInRole.Message, activeUserInRoleCount);
            throw new ValidationException(errorMessage, UserStatusCodes.ActiveUserInRole.StatusCode);
        }
        await _identityB2BService.DeleteBusinessRoleAsync(request.BusinessRoleRefId, cancellationToken);
    }
}


