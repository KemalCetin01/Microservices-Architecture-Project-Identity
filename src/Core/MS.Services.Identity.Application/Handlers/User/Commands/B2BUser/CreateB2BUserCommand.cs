using Amazon.S3.Model;
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

public class CreateB2BUserCommand : CreateB2BUserCommandDTO, ICommand<B2BUserGetByIdDTO>
{

}

public sealed class CreateB2BUserCommandHandler : BaseCommandHandler<CreateB2BUserCommand, B2BUserGetByIdDTO>
{
    private readonly IUserB2BService _userB2BService;
    private readonly ISectorService _sectorService;
    private readonly IUserEmployeeService _userEmployeeService;
    public CreateB2BUserCommandHandler(IUserB2BService userB2BService,
                                       ISectorService sectorService,
                                       IUserEmployeeService userEmployeeService)
    {
        _userB2BService = userB2BService;
        _sectorService = sectorService;
        _userEmployeeService = userEmployeeService;
    }

    public override async Task<B2BUserGetByIdDTO> Handle(CreateB2BUserCommand request, CancellationToken cancellationToken)
    {
        if (request.RepresentativeId.HasValue)
            await _userEmployeeService.GetByIdAsync(request.RepresentativeId.Value, cancellationToken);
        if (request.SectorId.HasValue)
            await _sectorService.GetByIdAsync(request.SectorId.Value, cancellationToken);
       
   
        return await _userB2BService.CreateAsync(request, cancellationToken);
    }
}