using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2CUser.ResetPassword;

public class B2CResetPasswordCommand : ResetPasswordCommandDTO, ICommand<VerifyOtpDTO>
{
}
public sealed class B2CResetPasswordCommandHandler : BaseCommandHandler<B2CResetPasswordCommand, VerifyOtpDTO>
{
    private readonly IAuthB2CService _authenticationService;

    public B2CResetPasswordCommandHandler(IAuthB2CService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    public override async Task<VerifyOtpDTO> Handle(B2CResetPasswordCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.ResetPasswordAsync(request, UserTypeEnum.B2C, cancellationToken);
    }
}

