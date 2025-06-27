using FluentValidation;
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2BUser.Login;

public class B2BRefreshTokenCommand : ICommand<AuthenticationDTO>
{
    public string RefreshToken { get; set; }

}
public sealed class B2BRefreshTokenCommandHandler : BaseCommandHandler<B2BRefreshTokenCommand, AuthenticationDTO>
{
    private readonly IAuthB2BService _authenticationService;

    public B2BRefreshTokenCommandHandler(IAuthB2BService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public override async Task<AuthenticationDTO> Handle(B2BRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.B2BRefreshTokenLoginAsync(request.RefreshToken, cancellationToken);
    }
}

public class B2BRefreshTokenLoginCommandValidator : AbstractValidator<B2BRefreshTokenCommand>
{
    public B2BRefreshTokenLoginCommandValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}
