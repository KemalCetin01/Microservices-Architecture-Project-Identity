using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

namespace MS.Services.Identity.Application.Handlers.Auth.Queries;

public class B2CValidateSignUpInfoQuery : B2CValidateSignUpInfoDTO, IQuery<DataResponse<bool>>
{

}
public sealed class B2CValidateSignUpInfoQueryHandler : BaseQueryHandler<B2CValidateSignUpInfoQuery, DataResponse<bool>>
{


    public B2CValidateSignUpInfoQueryHandler()
    {

    }

    public override async Task<DataResponse<bool>> Handle(B2CValidateSignUpInfoQuery request, CancellationToken cancellationToken)
    {


        return new DataResponse<bool>() { Data = true };
    }
}