using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2BUser.ResetPassword;

public class B2BResetPasswordCommand : ResetPasswordCommandDTO, ICommand<VerifyOtpDTO>
{
}
public sealed class B2BResetPasswordCommandHandler : BaseCommandHandler<B2BResetPasswordCommand, VerifyOtpDTO>
{
    private readonly IAuthB2BService _authenticationService;

    public B2BResetPasswordCommandHandler(IAuthB2BService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    public override async Task<VerifyOtpDTO> Handle(B2BResetPasswordCommand request, CancellationToken cancellationToken)
    {
        return await _authenticationService.ResetPasswordAsync(request, UserTypeEnum.B2B, cancellationToken);
    }
}

