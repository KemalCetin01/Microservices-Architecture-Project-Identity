using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Core.Base.Dtos.Response;

namespace MS.Services.Identity.Application.Handlers.CurrentAccounts.Queries
{
    public class GetSearchableCurrentAccountValueLabelQuery : IQuery<ListResponse<LabelValueResponse>>
    {
        public string Search { get; set; }
    }

    public sealed class GetCountriesQueryHandler : BaseQueryHandler<GetSearchableCurrentAccountValueLabelQuery, ListResponse<LabelValueResponse>>
    {
        protected readonly ICurrentAccountService _currentAccountService;
        protected readonly IMapper _mapper;

        public GetCountriesQueryHandler(ICurrentAccountService currentAccountService, IMapper mapper)
        {
            _currentAccountService = currentAccountService;
            _mapper = mapper;
        }

        public override async Task<ListResponse<LabelValueResponse>> Handle(GetSearchableCurrentAccountValueLabelQuery request,
            CancellationToken cancellationToken  )
        {
            var result = await _currentAccountService.GetAllAsync(request.Search, cancellationToken);
            return new ListResponse<LabelValueResponse>(_mapper.Map<ICollection<LabelValueResponse>>(result));
        }
    }
}
