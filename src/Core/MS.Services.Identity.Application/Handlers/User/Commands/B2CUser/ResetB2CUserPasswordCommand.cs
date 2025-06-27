using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.User.DTOs;

namespace MS.Services.Identity.Application.Handlers.User.Commands.B2CUser;

public class ResetB2CUserPasswordCommand : ResetPasswordCommandDTO, ICommand<DataResponse<bool>>
{
}
public sealed class ResetB2CUserPasswordCommandHandler : BaseCommandHandler<ResetB2CUserPasswordCommand, DataResponse<bool>>
{
    private readonly IUserB2CService _userB2CService;


    public ResetB2CUserPasswordCommandHandler(IUserB2CService userB2CService)
    {
        _userB2CService = userB2CService;
    }


    public override async Task<DataResponse<bool>> Handle(ResetB2CUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var result= await _userB2CService.ResetPasswordAsync(request, cancellationToken);
        return new DataResponse<bool> {Data= result };
    }
}

