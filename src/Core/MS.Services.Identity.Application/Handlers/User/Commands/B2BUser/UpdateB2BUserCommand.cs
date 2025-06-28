
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
    private readonly ISectorService _sectorService;
    private readonly IUserEmployeeService _userEmployeeService;
    public B2BUserCommandHandler(IUserB2BService userService,
                                 ISectorService sectorService,
                                 IUserEmployeeService userEmployeeService)
    {
        _userService = userService;
        _sectorService = sectorService;
        _userEmployeeService = userEmployeeService;
    }

    public override async Task<B2BUserGetByIdDTO> Handle(UpdateB2BUserCommand request, CancellationToken cancellationToken)
    {
        if (request.RepresentativeId.HasValue)
            await _userEmployeeService.GetByIdAsync(request.RepresentativeId.Value, cancellationToken);
        if (request.SectorId.HasValue)
            await _sectorService.GetByIdAsync(request.SectorId.Value, cancellationToken);

        return await _userService.UpdateAsync(request,cancellationToken);
    }
}
