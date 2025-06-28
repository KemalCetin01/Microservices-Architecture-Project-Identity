using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Auth.DTOs;
using MS.Services.Identity.Application.Handlers.Auth.DTOs.B2BUser;

namespace MS.Services.Identity.Application.Handlers.Auth.Commands.B2BUser.Register;

public class B2BSignUpCommand : B2BSignupCommandDTO, ICommand<SignUpDTO>
{
}
public sealed class B2BSignUpCommandHandler : BaseCommandHandler<B2BSignUpCommand, SignUpDTO>
{
    //private readonly IAuthB2BService _authenticationService;
    //private readonly IBusinessService _businessService;
    //private readonly ISectorService _sectorService;
    //private readonly IActivityAreaService _activityAreaService;
    //private readonly INumberOfEmployeeService _numberOfEmployeeService;
    //private readonly IPositionService _positionService;
    //private readonly IOccupationService _occupationService;
    //public B2BSignUpCommandHandler(IAuthB2BService authenticationService,
    //                               IBusinessService businessService,
    //                               ISectorService sectorService,
    //                               IActivityAreaService activityAreaService,
    //                               INumberOfEmployeeService numberOfEmployeeService,
    //                               IPositionService positionService,
    //                               IOccupationService occupationService)
    //{
    //    _authenticationService = authenticationService;
    //    _businessService = businessService;
    //    _sectorService = sectorService;
    //    _activityAreaService = activityAreaService;
    //    _numberOfEmployeeService = numberOfEmployeeService;
    //    _positionService = positionService;
    //    _occupationService = occupationService;
    //}

    private readonly IAuthB2BService _authenticationService;
    private readonly ISectorService _sectorService;
    public B2BSignUpCommandHandler(IAuthB2BService authenticationService,
                                   ISectorService sectorService)
    {
        _authenticationService = authenticationService;
        _sectorService = sectorService;
    }
    public override async Task<SignUpDTO> Handle(B2BSignUpCommand request, CancellationToken cancellationToken)
    {
        //await _businessService.CheckBusinessNameIfExists(request.CompanyName,Guid.Empty, cancellationToken);
        await _sectorService.GetByIdAsync(request.SectorId, cancellationToken);

        return await _authenticationService.B2BSignUpAsync(request, cancellationToken);
    }
}

