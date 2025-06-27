using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2CUser.Register;

public class B2CResetVerifyOtpCommand : VerifyOtpDTO, ICommand<ResetVerifyOtpDTO>
{
}
public sealed class B2CResetVerifyOtpCommandHandler : BaseCommandHandler<B2CResetVerifyOtpCommand, ResetVerifyOtpDTO>
{
    private readonly IAuthB2CService _authenticationService;

    public B2CResetVerifyOtpCommandHandler(IAuthB2CService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    public override async Task<ResetVerifyOtpDTO> Handle(B2CResetVerifyOtpCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.ResetVerifyOtpAsync(request, UserTypeEnum.B2C, cancellationToken);
    }
}

