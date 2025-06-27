using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Sectors.DTOs;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.Sectors.Commands;

public class CreateSectorCommand:ICommand<SectorDTO>
{
    public string Name { get; set; }
    public StatusEnum Status { get; set; }

}
public sealed class CreateSectorCommandHandler : BaseCommandHandler<CreateSectorCommand, SectorDTO>
{
    private readonly ISectorService _sectorService;

    public CreateSectorCommandHandler(ISectorService sectorService)
    {
        _sectorService = sectorService;
    }


    public override async Task<SectorDTO> Handle(CreateSectorCommand request, CancellationToken cancellationToken)
    {
        return await _sectorService.AddAsync(request, cancellationToken);
    }
}

