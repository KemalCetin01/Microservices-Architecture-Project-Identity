using AutoMapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;

namespace MS.Services.Identity.Application.Handlers.Occupations.Queries;

public sealed class GetOccupationsQuery : IQuery<ListResponse<LabelIntValueResponse>>
{

}

public sealed class GetOccupationsQueryHandler : BaseQueryHandler<GetOccupationsQuery, ListResponse<LabelIntValueResponse>>
{
    protected readonly IOccupationService _occupationService;
    protected readonly IMapper _mapper;

    public GetOccupationsQueryHandler(IOccupationService occupationService, IMapper mapper)
    {
        _occupationService = occupationService;
        _mapper = mapper;
    }

    public override async Task<ListResponse<LabelIntValueResponse>> Handle(GetOccupationsQuery request, CancellationToken cancellationToken  )
    {
        var result = await _occupationService.GetFKeyListAsync(cancellationToken);
        return new ListResponse<LabelIntValueResponse>(_mapper.Map<ICollection<LabelIntValueResponse>>(result));
    }

}