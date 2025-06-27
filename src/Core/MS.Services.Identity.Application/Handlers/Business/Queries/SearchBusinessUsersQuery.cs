using AutoMapper;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.User.DTOs;
using MS.Services.Identity.Domain.EntityFilters;
using FluentValidation;
using MS.Services.Identity.Domain.EntityConstants;
using MS.Services.Identity.Application.DTOs.Business.Response;

namespace MS.Services.Identity.Application.Handlers.Business.Queries;

public sealed class SearchBusinessUsersQuery : SearchQuery<SearchBusinessUsersQueryFilter, PagedResponse<SearchBusinessUsersResponseDto>>
{
}

public sealed class SearchBusinessUsersQueryHandler : BaseQueryHandler<SearchBusinessUsersQuery, PagedResponse<SearchBusinessUsersResponseDto>>
{
    private readonly IUserB2BService _userB2BService;
    private readonly IMapper _mapper;

    public SearchBusinessUsersQueryHandler(IUserB2BService userB2BService, IMapper mapper)
    {
        _userB2BService = userB2BService;
        _mapper = mapper;
    }

    public override async Task<PagedResponse<SearchBusinessUsersResponseDto>> Handle(SearchBusinessUsersQuery request, CancellationToken cancellationToken)
    {
        var searchQueryModel = _mapper.Map<SearchQueryModel<BusinessUserSearchFilter>>(request);

        return await _userB2BService.SearchBusinessUsersAsync(searchQueryModel, cancellationToken);

    }
}

public sealed class SearchBusinessUsersQueryFilter : IFilter
{
    public Guid BusinessId { get; set; }
    public string? FullName { get; set; }
    public string? Email { get; set; }
}

public sealed class SearchBusinessUsersQueryValidator : AbstractValidator<SearchBusinessUsersQuery>
{
    public SearchBusinessUsersQueryValidator()
    {
        RuleFor(x => x.Filter)
            .NotNull()
            .DependentRules(() =>
            {
                RuleFor(x => x.Filter!.BusinessId).NotEmpty();
                RuleFor(x => x.Filter!.Email).MaximumLength(B2BUserConstants.EmailMaxLength);
                RuleFor(x => x.Filter!.FullName).MaximumLength(B2BUserConstants.FullNameMaxLength);
            });
    }
}


