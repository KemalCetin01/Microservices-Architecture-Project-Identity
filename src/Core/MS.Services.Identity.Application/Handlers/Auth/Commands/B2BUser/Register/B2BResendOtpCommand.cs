using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2CUser.Register;

public class B2BResendOtpCommand :  ICommand<ResendOtpDTO>
{
}
public sealed class B2BResendOtpCommandHandler : BaseCommandHandler<B2BResendOtpCommand, ResendOtpDTO>
{
    private readonly IAuthB2BService _authenticationService;

    public B2BResendOtpCommandHandler(IAuthB2BService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public override async Task<ResendOtpDTO> Handle(B2BResendOtpCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.B2BResendOtpAsync(cancellationToken);
    }
}

