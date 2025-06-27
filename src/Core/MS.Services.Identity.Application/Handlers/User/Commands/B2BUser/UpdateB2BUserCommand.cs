
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.User.DTOs;

namespace MS.Services.Identity.Application.Handlers.User.Commands.B2BUser;

public class UpdateB2BUserCommand : UpdateB2BUserCommandDTO, ICommand<B2BUserGetByIdDTO>
{
  
}

public sealed class B2BUserCommandHandler : BaseCommandHandler<UpdateB2BUserCommand, B2BUserGetByIdDTO>
{
    private readonly IUserB2BService _userService;
    private readonly IBusinessService _businessService;
    private readonly ISectorService _sectorService;
    private readonly IActivityAreaService _activityAreaService;
    private readonly INumberOfEmployeeService _numberOfEmployeeService;
    private readonly IPositionService _positionService;
    private readonly IOccupationService _occupationService;
    private readonly IUserEmployeeService _userEmployeeService;
    public B2BUserCommandHandler(IUserB2BService userService,
                                 IBusinessService businessService,
                                 ISectorService sectorService,
                                 IActivityAreaService activityAreaService,
                                 INumberOfEmployeeService numberOfEmployeeService,
                                 IPositionService positionService,
                                 IOccupationService occupationService,
                                 IUserEmployeeService userEmployeeService)
    {
        _userService = userService;
        _businessService = businessService;
        _sectorService = sectorService;
        _activityAreaService = activityAreaService;
        _numberOfEmployeeService = numberOfEmployeeService;
        _positionService = positionService;
        _occupationService = occupationService;
        _userEmployeeService = userEmployeeService;
    }

    public override async Task<B2BUserGetByIdDTO> Handle(UpdateB2BUserCommand request, CancellationToken cancellationToken)
    {
        if (request.RepresentativeId.HasValue)
            await _userEmployeeService.GetByIdAsync(request.RepresentativeId.Value, cancellationToken);
        if (request.SectorId.HasValue)
            await _sectorService.GetByIdAsync(request.SectorId.Value, cancellationToken);
        if (request.ActivityAreaId.HasValue)
            await _activityAreaService.GetByIdAsync(request.ActivityAreaId.Value, cancellationToken);

        return await _userService.UpdateAsync(request,cancellationToken);
    }
}
