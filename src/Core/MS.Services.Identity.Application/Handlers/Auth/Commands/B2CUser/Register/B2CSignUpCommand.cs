using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2CUser.Register;

public class B2CSignUpCommand : B2CSignUpCommandDTO, ICommand<SignUpDTO>
{
}
public sealed class B2CSignUpCommandHandler : BaseCommandHandler<B2CSignUpCommand, SignUpDTO>
{
    private readonly IAuthB2CService _authenticationService;

    public B2CSignUpCommandHandler(IAuthB2CService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public override async Task<SignUpDTO> Handle(B2CSignUpCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.B2CSignUpAsync(request, cancellationToken);
    }
}

