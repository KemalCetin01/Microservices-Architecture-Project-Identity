using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Positions.DTOs;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.Positions.Commands;

public class UpdatePositionCommand : ICommand<PositionDTO>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public StatusEnum Status{ get; set; }
}
public sealed class UpdatePositionCommandHandler : BaseCommandHandler<UpdatePositionCommand, PositionDTO>
{
    private readonly IPositionService _positionService;

    public UpdatePositionCommandHandler(IPositionService positionService)
    {
        _positionService = positionService;
    }

    public override async Task<PositionDTO> Handle(UpdatePositionCommand request, CancellationToken cancellationToken)
    {
        return await _positionService.UpdateAsync(request, cancellationToken);
    }
}