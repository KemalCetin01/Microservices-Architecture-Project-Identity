using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Sectors.DTOs;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.Sectors.Commands;

public class UpdateSectorCommand : ICommand<SectorDTO>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public StatusEnum Status{ get; set; }
}
public sealed class UpdateSectorCommandHandler : BaseCommandHandler<UpdateSectorCommand, SectorDTO>
{
    private readonly ISectorService _sectorService;

    public UpdateSectorCommandHandler(ISectorService sectorService)
    {
        _sectorService = sectorService;
    }

    public override async Task<SectorDTO> Handle(UpdateSectorCommand request, CancellationToken cancellationToken)
    {
        return await _sectorService.UpdateAsync(request, cancellationToken);
    }
}