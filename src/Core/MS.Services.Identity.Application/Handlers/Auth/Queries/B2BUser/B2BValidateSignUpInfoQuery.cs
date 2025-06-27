using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

namespace MS.Services.Identity.Application.Handlers.Auth.Queries;

public class B2BValidateSignUpInfoQuery : B2BValidateSignUpInfoDTO, IQuery<DataResponse<bool>>
{

}
public sealed class B2BValidateSignUpInfoQueryHandler : BaseQueryHandler<B2BValidateSignUpInfoQuery, DataResponse<bool>>
{
    private readonly IBusinessService _businessService;
    private readonly ISectorService _sectorService;
    private readonly IActivityAreaService _activityAreaService;
    private readonly INumberOfEmployeeService _numberOfEmployeeService;
    private readonly IPositionService _positionService;
    private readonly IOccupationService _occupationService;

    public B2BValidateSignUpInfoQueryHandler(IBusinessService businessService,
                                                ISectorService sectorService,
                                                IActivityAreaService activityAreaService,
                                                INumberOfEmployeeService numberOfEmployeeService,
                                                IPositionService positionService,
                                                IOccupationService occupationService)
    {
        _businessService = businessService;
        _sectorService = sectorService;
        _activityAreaService = activityAreaService;
        _numberOfEmployeeService = numberOfEmployeeService;
        _positionService = positionService;
        _occupationService = occupationService;
    }

    public override async Task<DataResponse<bool>> Handle(B2BValidateSignUpInfoQuery request, CancellationToken cancellationToken)
    {
        //await _businessService.CheckBusinessNameIfExists(request.CompanyName,Guid.Empty, cancellationToken);
        await _sectorService.GetByIdAsync(request.SectorId, cancellationToken);
        await _activityAreaService.GetByIdAsync(request.ActivityAreaId, cancellationToken);
        await _numberOfEmployeeService.GetByIdAsync(request.NumberOfEmployeeId, cancellationToken);
        await _positionService.GetByIdAsync(request.PositionId, cancellationToken);
        await _occupationService.GetByIdAsync(request.OccupationId, cancellationToken);

        return new DataResponse<bool>() { Data = true };
    }
}