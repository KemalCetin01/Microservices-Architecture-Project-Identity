using FluentValidation;
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2BUser.Login;

public class B2BLoginCommand : B2BLoginDTO, ICommand<AuthenticationDTO>
{
}
public sealed class B2BLoginCommandHandler : BaseCommandHandler<B2BLoginCommand, AuthenticationDTO>
{
    private readonly IAuthB2BService _authenticationService;

    public B2BLoginCommandHandler(IAuthB2BService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    public override async Task<AuthenticationDTO> Handle(B2BLoginCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.B2BLoginAsync(request, cancellationToken);
    }
}

public class B2BLoginCommandValidator : AbstractValidator<B2BLoginCommand>
{
    public B2BLoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}

