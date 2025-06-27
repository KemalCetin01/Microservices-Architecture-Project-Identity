using AutoMapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;

namespace MS.Services.Identity.Application.Handlers.Sectors.Queries;

public sealed class GetSectorsQuery : IQuery<ListResponse<LabelIntValueResponse>>
{

}

public sealed class GetSectorsQueryHandler : BaseQueryHandler<GetSectorsQuery, ListResponse<LabelIntValueResponse>>
{
    protected readonly ISectorService _sectorService;
    protected readonly IMapper _mapper;

    public GetSectorsQueryHandler(ISectorService sectorService, IMapper mapper)
    {
        _sectorService = sectorService;
        _mapper = mapper;
    }

    public override async Task<ListResponse<LabelIntValueResponse>> Handle(GetSectorsQuery request, CancellationToken cancellationToken  )
    {
        var result = await _sectorService.GetFKeyListAsync(cancellationToken);
        return new ListResponse<LabelIntValueResponse>(_mapper.Map<ICollection<LabelIntValueResponse>>(result));
    }

}