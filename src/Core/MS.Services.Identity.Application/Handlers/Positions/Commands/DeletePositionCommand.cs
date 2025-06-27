using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.Positions.Commands;

public class DeletePositionCommand : ICommand
{
    public int Id { get; set; }
}
public sealed class DeletePositionCommandHandler : BaseCommandHandler<DeletePositionCommand>
{
    private readonly IPositionService _positionService;

    public DeletePositionCommandHandler(IPositionService positionService)
    {
        _positionService = positionService;
    }


    public override async Task Handle(DeletePositionCommand request, CancellationToken cancellationToken)
    {
        await _positionService.DeleteAsync(request.Id, cancellationToken);
    }
}
