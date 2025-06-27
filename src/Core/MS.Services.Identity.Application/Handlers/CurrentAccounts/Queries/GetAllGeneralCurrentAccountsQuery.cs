using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.DTOs;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.Handlers.CurrentAccounts.Queries;

public class GetAllGeneralCurrentAccountsQuery : SearchQuery<GetAllGeneralCurrentAccountsQueryFilter, PagedResponse<GeneralCurrentAccountsDTO>>// IQuery<PagedResponse<GeneralCurrentAccountsDTO>>
{
}

public class GetAllGeneralCurrentAccountsQueryFilter : IFilter
{
    public Guid? CurrentId { get; set; }
    public string? currentAccountName { get; set; }
    public string? CurrentCode { get; set; }
    public AvailabilityEnum? CurrentAccountStatus { get; set; } // 0 kullanımda - 1 kullanım Dışı  
    public SiteStatusEnum? SiteStatus { get; set; }//1 aktif 0 pasif
    public string? TransactionCurrency { get; set; } //TODO:? İşlem Dövizi
    public string? ExchangeRate { get; set; } // İşlem Döviz Kuru
    public AccessStatusEnum SalesAndDistribution { get; set; }//1 açık 0 kapalı
}
public sealed class GetAllGeneralCurrentAccountsQueryHandler : BaseQueryHandler<GetAllGeneralCurrentAccountsQuery, PagedResponse<GeneralCurrentAccountsDTO>>
{
    protected readonly ICurrentAccountService _currentAccountService;
    private readonly IMapper _mapper;

    public GetAllGeneralCurrentAccountsQueryHandler(ICurrentAccountService currentAccountService, IMapper mapper)
    {
        _currentAccountService = currentAccountService;
        _mapper = mapper;
    }

    public override async Task<PagedResponse<GeneralCurrentAccountsDTO>> Handle(GetAllGeneralCurrentAccountsQuery request, CancellationToken cancellationToken)
    {
        var searchResult = _mapper.Map<SearchQueryModel<GetAllGeneralCurrentAccountsQueryFilterModel>>(request);

        var result = await _currentAccountService.GetGeneralCurrentAsync(searchResult, cancellationToken);
        return result;
    }
}
