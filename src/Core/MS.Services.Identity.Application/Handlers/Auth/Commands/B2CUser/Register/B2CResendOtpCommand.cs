using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2CUser;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2CUser.Register;

public class B2CResendOtpCommand :  ICommand<ResendOtpDTO>
{
}
public sealed class B2CResendOtpCommandHandler : BaseCommandHandler<B2CResendOtpCommand, ResendOtpDTO>
{
    private readonly IAuthB2CService _authenticationService;

    public B2CResendOtpCommandHandler(IAuthB2CService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    public override async Task<ResendOtpDTO> Handle(B2CResendOtpCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.B2CResendOtpAsync( cancellationToken);
    }
}

