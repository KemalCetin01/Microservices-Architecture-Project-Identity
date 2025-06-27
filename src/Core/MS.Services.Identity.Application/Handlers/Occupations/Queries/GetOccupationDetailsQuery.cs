using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Occupations.DTOs;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.Occupations.Queries;

public class GetOccupationDetailsQuery:IQuery<OccupationDTO>
{
    public int Id { get; set; }
}
public sealed class GetOccupationDetailQueryHandler : BaseQueryHandler<GetOccupationDetailsQuery, OccupationDTO>
{
    protected readonly IOccupationService _occupationService;

    public GetOccupationDetailQueryHandler(IOccupationService occupationService)
    {
        _occupationService = occupationService;
    }

    public override async Task<OccupationDTO> Handle(GetOccupationDetailsQuery request, CancellationToken cancellationToken  )
    {
      return await _occupationService.GetByIdAsync(request.Id, cancellationToken);
    }
}