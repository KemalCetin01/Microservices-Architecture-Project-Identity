using MS.Services.Core.Base.Handlers;
//using MS.Services.Core.Localization.Mapping;
//using MS.Services.Core.Localization.Services.Interfaces;
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
    //private readonly ILocalizationMapper _localizationMapper;

    public GetSectorDetailQueryHandler(ISectorService sectorService/*, ILocalizationMapper localizationMapper*/)
    {
        _sectorService = sectorService;
      //  _localizationMapper = localizationMapper;
    }

    public override async Task<SectorDTO> Handle(GetSectorDetailsQuery request, CancellationToken cancellationToken  )
    {
        return await _sectorService.GetByIdAsync(request.Id, cancellationToken);

    }
}