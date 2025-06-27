using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.User.DTOs;

namespace MS.Services.Identity.Application.Handlers.User.Commands.B2CUser;

public class CreateB2CUserCommand : CreateB2CUserCommandDTO, ICommand<B2CUserGetByIdDTO>
{
  
}
public sealed class CreateB2CUserCommandHandler : BaseCommandHandler<CreateB2CUserCommand, B2CUserGetByIdDTO>
{
    private readonly IUserB2CService _userB2CService;
    private readonly ISectorService _sectorService;
    private readonly IActivityAreaService _activityAreaService;
    private readonly IOccupationService _occupationService;
    private readonly IUserEmployeeService _userEmployeeService;

    public CreateB2CUserCommandHandler(IUserB2CService userB2CService,
                                   ISectorService sectorService,
                                   IActivityAreaService activityAreaService,
                                   IOccupationService occupationService,
                                   IUserEmployeeService userEmployeeService)
    {
        _userB2CService = userB2CService;
        _sectorService = sectorService;
        _activityAreaService = activityAreaService;
        _occupationService = occupationService;
        _userEmployeeService = userEmployeeService;
    }

    public override async Task<B2CUserGetByIdDTO> Handle(CreateB2CUserCommand request, CancellationToken cancellationToken)
    {
        if (request.RepresentativeId.HasValue)
            await _userEmployeeService.GetByIdAsync(request.RepresentativeId.Value, cancellationToken);
        if (request.SectorId.HasValue)
            await _sectorService.GetByIdAsync(request.SectorId.Value, cancellationToken);
        if (request.ActivityAreaId.HasValue)
            await _activityAreaService.GetByIdAsync(request.ActivityAreaId.Value, cancellationToken);
        if (request.OccupationId.HasValue)
            await _occupationService.GetByIdAsync(request.OccupationId.Value, cancellationToken);

        return await _userB2CService.CreateAsync(request, cancellationToken);
    }
}
