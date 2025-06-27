using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Positions.DTOs;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.Positions.Commands;

public class CreatePositionCommand:ICommand<PositionDTO>
{
    public string Name { get; set; }
    public StatusEnum Status { get; set; }

}
public sealed class CreatePositionCommandHandler : BaseCommandHandler<CreatePositionCommand, PositionDTO>
{
    private readonly IPositionService _positionService;

    public CreatePositionCommandHandler(IPositionService positionService)
    {
        _positionService = positionService;
    }


    public override async Task<PositionDTO> Handle(CreatePositionCommand request, CancellationToken cancellationToken)
    {
        return await _positionService.AddAsync(request, cancellationToken);
    }
}

