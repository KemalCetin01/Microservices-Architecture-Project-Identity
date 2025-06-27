using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.Positions.DTOs;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.Positions.Queries;

public class GetPositionDetailsQuery:IQuery<PositionDTO>
{
    public int Id { get; set; }
}
public sealed class GetPositionDetailQueryHandler : BaseQueryHandler<GetPositionDetailsQuery, PositionDTO>
{
    protected readonly IPositionService _positionService;

    public GetPositionDetailQueryHandler(IPositionService positionService)
    {
        _positionService = positionService;
    }

    public override async Task<PositionDTO> Handle(GetPositionDetailsQuery request, CancellationToken cancellationToken  )
    {
      return await _positionService.GetByIdAsync(request.Id, cancellationToken);
    }
}