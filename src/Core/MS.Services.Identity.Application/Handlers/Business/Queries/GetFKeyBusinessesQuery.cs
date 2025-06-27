using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Dtos.Response;

namespace MS.Services.Identity.Application.Handlers.Business.Queries;

public sealed class GetFKeyBusinessesQuery : IQuery<ListResponse<LabelValueResponse>>
{
    public string? Search { get; set; }
}

public sealed class GetFKeyBusinessesQueryHandler : BaseQueryHandler<GetFKeyBusinessesQuery, ListResponse<LabelValueResponse>>
{
    private readonly IBusinessService _businessService;
    private readonly IMapper _mapper;

    public GetFKeyBusinessesQueryHandler(IBusinessService businessServvice, IMapper mapper)
    {
        _businessService = businessServvice;
        _mapper = mapper;
    }

    public override async Task<ListResponse<LabelValueResponse>> Handle(GetFKeyBusinessesQuery request,
        CancellationToken cancellationToken  )
    {
        return await _businessService.GetFKeyBusinessesAsync(request.Search, cancellationToken);
    }
}