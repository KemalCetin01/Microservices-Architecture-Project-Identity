using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Sectors.DTOs;

namespace MS.Services.Identity.Application.Handlers.Sectors.Queries;

public class GetSectorDetailsQuery:IQuery<SectorDTO>
{
    public int Id { get; set; }
}
public sealed class GetSectorDetailQueryHandler : BaseQueryHandler<GetSectorDetailsQuery, SectorDTO>
{
    protected readonly ISectorService _sectorService;

    public GetSectorDetailQueryHandler(ISectorService sectorService)
    {
        _sectorService = sectorService;
    }

    public override async Task<SectorDTO> Handle(GetSectorDetailsQuery request, CancellationToken cancellationToken  )
    {
        return await _sectorService.GetByIdAsync(request.Id, cancellationToken);

    }
}