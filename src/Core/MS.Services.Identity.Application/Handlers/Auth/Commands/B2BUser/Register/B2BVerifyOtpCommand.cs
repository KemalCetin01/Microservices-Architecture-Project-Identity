using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2BUser.Register;

public class B2BVerifyOtpCommand : VerifyOtpDTO, ICommand<DataResponse<bool>>
{
}
public sealed class B2BVerifyOtpCommandHandler : BaseCommandHandler<B2BVerifyOtpCommand, DataResponse<bool>>
{
    private readonly IAuthB2BService _authenticationService;

    public B2BVerifyOtpCommandHandler(IAuthB2BService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    public override async Task<DataResponse<bool>> Handle(B2BVerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var result= await _authenticationService.B2BVerifyOtpAsync(request, cancellationToken);
        return new DataResponse<bool> { Data = result };

    }
}

