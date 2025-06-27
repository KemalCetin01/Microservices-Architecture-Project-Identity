using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.User.DTOs;

namespace MS.Services.Identity.Application.Handlers.User.Commands.B2BUser;

public class ResetB2BUserPasswordCommand : ResetPasswordCommandDTO, ICommand<DataResponse<bool>>
{
}
public sealed class ResetB2BUserPasswordCommandHandler : BaseCommandHandler<ResetB2BUserPasswordCommand, DataResponse<bool>>
{
    private readonly IUserB2BService _userB2BService;


    public ResetB2BUserPasswordCommandHandler(IUserB2BService userB2BService)
    {
        _userB2BService = userB2BService;
    }


    public override async Task<DataResponse<bool>> Handle(ResetB2BUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var result= await _userB2BService.ResetPasswordAsync(request, cancellationToken);
        return new DataResponse<bool> {Data= result };
    }
}

