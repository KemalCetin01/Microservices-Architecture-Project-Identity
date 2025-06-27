using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2CUser.Register;

public class B2CMoreQuestionsCommand : B2CMoreQuestionsCommandDTO, ICommand<DataResponse<bool>>
{
}
public sealed class B2CMoreQuestionsCommandHandler : BaseCommandHandler<B2CMoreQuestionsCommand, DataResponse<bool>>
{
    private readonly IAuthB2CService _authenticationService;
    private readonly ISectorService _sectorService;
    private readonly IActivityAreaService _activityAreaService;
    private readonly IOccupationService _occupationService;
    public B2CMoreQuestionsCommandHandler(IAuthB2CService authenticationService,
                                            ISectorService sectorService,
                                            IActivityAreaService activityAreaService,
                                            IOccupationService occupationService)
    {
        _authenticationService = authenticationService;
        _sectorService = sectorService;
        _activityAreaService = activityAreaService;
        _occupationService = occupationService;
    }


    public override async Task<DataResponse<bool>> Handle(B2CMoreQuestionsCommand request, CancellationToken cancellationToken)
    {
        await _sectorService.GetByIdAsync(request.SectorId, cancellationToken);
        await _activityAreaService.GetByIdAsync(request.ActivityAreaId, cancellationToken);
        await _occupationService.GetByIdAsync(request.OccupationId, cancellationToken);

        var result = await _authenticationService.B2CMoreQuestionsAsync(request, cancellationToken);

        return new DataResponse<bool> { Data = result };
    }
}

