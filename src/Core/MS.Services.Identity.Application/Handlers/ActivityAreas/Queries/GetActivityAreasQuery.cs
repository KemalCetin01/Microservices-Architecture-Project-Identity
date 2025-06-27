using AutoMapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Base.Dtos.Response;

namespace MS.Services.Identity.Application.Handlers.ActivityAreas.Queries;

public sealed class GetActivityAreasQuery : IQuery<ListResponse<LabelIntValueResponse>>
{

}

public sealed class GetActivityAreasQueryHandler : BaseQueryHandler<GetActivityAreasQuery, ListResponse<LabelIntValueResponse>>
{
    protected readonly IActivityAreaService _activityAreaService;
    protected readonly IMapper _mapper;

    public GetActivityAreasQueryHandler(IActivityAreaService activityAreaService, IMapper mapper)
    {
        _activityAreaService = activityAreaService;
        _mapper = mapper;
    }

    public override async Task<ListResponse<LabelIntValueResponse>> Handle(GetActivityAreasQuery request, CancellationToken cancellationToken  )
    {
        var result = await _activityAreaService.GetFKeyListAsync(cancellationToken);
        return new ListResponse<LabelIntValueResponse>(_mapper.Map<ICollection<LabelIntValueResponse>>(result));
    }

}