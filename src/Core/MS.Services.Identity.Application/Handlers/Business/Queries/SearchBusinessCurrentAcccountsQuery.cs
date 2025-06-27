using AutoMapper;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.User.DTOs;
using MS.Services.Identity.Domain.Filters.UserB2BFilters;
using FluentValidation;
using MS.Services.Identity.Domain.EntityConstants;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Handlers.Business.Queries;

public sealed class SearchBusinessCurrentAcccountsQuery : SearchQuery<SearchBusinessCurrentAcccountsQueryFilter, PagedResponse<SearchBusinessCurrentAccountsResponseDto>>
{
}

public sealed class SearchBusinessCurrentAcccountsQueryHandler : BaseQueryHandler<SearchBusinessCurrentAcccountsQuery, PagedResponse<SearchBusinessCurrentAccountsResponseDto>>
{
    private readonly IBusinessCurrentAccountService _businessCurrentAccountService;
    private readonly IMapper _mapper;

    public SearchBusinessCurrentAcccountsQueryHandler(IBusinessCurrentAccountService businessCurrentAccountService, IMapper mapper)
    {
        _businessCurrentAccountService = businessCurrentAccountService;
        _mapper = mapper;
    }

    public override async Task<PagedResponse<SearchBusinessCurrentAccountsResponseDto>> Handle(SearchBusinessCurrentAcccountsQuery request, CancellationToken cancellationToken)
    {
        var searchQueryModel = _mapper.Map<SearchQueryModel<BusinessCurrentAccountSearchFilter>>(request);
        return await _businessCurrentAccountService.SearchAsync(searchQueryModel, cancellationToken);
    }
}

public sealed class SearchBusinessCurrentAcccountsQueryFilter : IFilter
{
    public Guid BusinessId { get; set; }
    public string? ErpRefId { get; set; }
    public string? Code { get; set; }
    public string? CurrentAccountName { get; set; }
}

public sealed class SearchBusinessCurrentAcccountsQueryValidator : AbstractValidator<SearchBusinessCurrentAcccountsQuery>
{
    public SearchBusinessCurrentAcccountsQueryValidator()
    {
        RuleFor(x => x.Filter)
            .NotNull()
            .DependentRules(() =>
            {
                RuleFor(x => x.Filter!.BusinessId).NotEmpty();
                RuleFor(x => x.Filter!.ErpRefId).MaximumLength(CurrentAccountConstants.ErpRefIdMaxLength);
                RuleFor(x => x.Filter!.Code).MaximumLength(CurrentAccountConstants.CodeMaxLength);
                RuleFor(x => x.Filter!.CurrentAccountName).MaximumLength(CurrentAccountConstants.NameMaxLength);
            });
    }
}


