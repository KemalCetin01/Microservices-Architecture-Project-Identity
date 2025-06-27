using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.Sectors.Commands;

public class DeleteSectorCommand : ICommand
{
    public int Id { get; set; }
}
public sealed class DeleteSectorCommandHandler : BaseCommandHandler<DeleteSectorCommand>
{
    private readonly ISectorService _sectorService;

    public DeleteSectorCommandHandler(ISectorService sectorService)
    {
        _sectorService = sectorService;
    }


    public override async Task Handle(DeleteSectorCommand request, CancellationToken cancellationToken)
    {
        await _sectorService.DeleteAsync(request.Id, cancellationToken);
    }
}
