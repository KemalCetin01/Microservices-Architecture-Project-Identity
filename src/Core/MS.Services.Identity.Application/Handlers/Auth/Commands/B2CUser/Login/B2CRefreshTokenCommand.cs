using FluentValidation;
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2CUser.Login;

public class B2CRefreshTokenCommand : ICommand<AuthenticationDTO>
{
    public string RefreshToken { get; set; }

}
public sealed class B2CRefreshTokenCommandHandler : BaseCommandHandler<B2CRefreshTokenCommand, AuthenticationDTO>
{
    private readonly IAuthB2CService _authenticationService;

    public B2CRefreshTokenCommandHandler(IAuthB2CService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public override async Task<AuthenticationDTO> Handle(B2CRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.B2CRefreshTokenLoginAsync(request.RefreshToken, cancellationToken);
    }
}

public class B2CRefreshTokenLoginCommandValidator : AbstractValidator<B2CRefreshTokenCommand>
{
    public B2CRefreshTokenLoginCommandValidator()
    {
        RuleFor(x => x.RefreshToken).NotEmpty();
    }
}
