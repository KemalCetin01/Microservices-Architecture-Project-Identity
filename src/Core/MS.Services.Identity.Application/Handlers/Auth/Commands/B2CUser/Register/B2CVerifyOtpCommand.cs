using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2CUser.Register;

public class B2CVerifyOtpCommand : VerifyOtpDTO, ICommand<DataResponse<bool>>
{
}
public sealed class B2CVerifyOtpCommandHandler : BaseCommandHandler<B2CVerifyOtpCommand, DataResponse<bool>>
{
    private readonly IAuthB2CService _authenticationService;

    public B2CVerifyOtpCommandHandler(IAuthB2CService authenticationService)
    {
        _authenticationService = authenticationService;
    }


    public override async Task<DataResponse<bool>> Handle(B2CVerifyOtpCommand request, CancellationToken cancellationToken)
    {
        var result= await _authenticationService.B2CVerifyOtpAsync(request, cancellationToken);

        return new DataResponse<bool> { Data=result };
    }
}

