using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.ActivityAreas.DTOs;
using MS.Services.Core.Base.Handlers;

namespace MS.Services.Identity.Application.Handlers.ActivityAreas.Queries;

public class GetActivityAreaDetailsQuery : IQuery<ActivityAreaDTO>
{
    public int Id { get; set; }
}
public sealed class GetActivityAreaDetailsQueryHandler : BaseQueryHandler<GetActivityAreaDetailsQuery, ActivityAreaDTO>
{
    protected readonly IActivityAreaService _activityAreaService;

    public GetActivityAreaDetailsQueryHandler(IActivityAreaService activityAreaService)
    {
        _activityAreaService = activityAreaService;
    }

    public override async Task<ActivityAreaDTO> Handle(GetActivityAreaDetailsQuery request, CancellationToken cancellationToken  )
    {
        return await _activityAreaService.GetByIdAsync(request.Id, cancellationToken);
    }
}