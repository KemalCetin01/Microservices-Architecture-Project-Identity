using AutoMapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;

namespace MS.Services.Identity.Application.Handlers.Positions.Queries;

public sealed class GetPositionsQuery : IQuery<ListResponse<LabelIntValueResponse>>
{

}

public sealed class GetPositionsQueryHandler : BaseQueryHandler<GetPositionsQuery, ListResponse<LabelIntValueResponse>>
{
    protected readonly IPositionService _positionService;
    protected readonly IMapper _mapper;

    public GetPositionsQueryHandler(IPositionService positionService, IMapper mapper)
    {
        _positionService = positionService;
        _mapper = mapper;
    }

    public override async Task<ListResponse<LabelIntValueResponse>> Handle(GetPositionsQuery request, CancellationToken cancellationToken  )
    {
        var result = await _positionService.GetFKeyListAsync(cancellationToken);
        return new ListResponse<LabelIntValueResponse>(_mapper.Map<ICollection<LabelIntValueResponse>>(result));
    }

}