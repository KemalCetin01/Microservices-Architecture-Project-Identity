using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;

namespace MS.Services.Identity.Application.Handlers.User.Commands.B2BUser;

public class DeleteB2BUserCommand : ICommand
{
    public Guid Id { get; set; }
}
public sealed class DeleteB2BUserCommandHandler : BaseCommandHandler<DeleteB2BUserCommand>
{
    private readonly IUserB2BService _userService;
    public DeleteB2BUserCommandHandler(IUserB2BService userService)
    {
        _userService = userService;
    }
    public override async Task Handle(DeleteB2BUserCommand request, CancellationToken cancellationToken)
    {
        await _userService.DeleteAsync(request.Id, cancellationToken);
    }
}
