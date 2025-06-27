using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;

namespace MS.Services.Identity.Application.Handlers.User.Commands.B2CUser
{
    public class DeleteB2CUserCommand : ICommand
    {
        public Guid Id { get; set; }
    }

    public sealed class DeleteB2CUserCommandHandler : BaseCommandHandler<DeleteB2CUserCommand>
    {
        private readonly IUserB2CService _userService;
        public DeleteB2CUserCommandHandler(IUserB2CService userService)
        {
            _userService = userService;
        }
        public override async Task Handle(DeleteB2CUserCommand request, CancellationToken cancellationToken)
        {
            await _userService.DeleteAsync(request.Id,cancellationToken);
        }
    }
}
