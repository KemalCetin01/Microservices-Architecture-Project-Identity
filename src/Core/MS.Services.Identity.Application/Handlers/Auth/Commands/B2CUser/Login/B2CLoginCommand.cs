using FluentValidation;
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2CUser;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2CUser.Login;

public class B2CLoginCommand : B2CLoginDTO, ICommand<AuthenticationDTO>
{
}
public sealed class LoginCommandHandler : BaseCommandHandler<B2CLoginCommand, AuthenticationDTO>
{
    private readonly IAuthB2CService _authenticationService;

    public LoginCommandHandler(IAuthB2CService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    public override async Task<AuthenticationDTO> Handle(B2CLoginCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.B2CLoginAsync(request, cancellationToken);
    }
}
public class B2CLoginCommandValidator : AbstractValidator<B2CLoginCommand>
{
    public B2CLoginCommandValidator()
    {
        RuleFor(x => x.EmailOrPhone).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}

