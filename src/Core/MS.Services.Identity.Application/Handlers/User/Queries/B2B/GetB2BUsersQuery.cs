

using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.User.DTOs;
using MS.Services.Identity.Application.Handlers.User.Queries.Filters;
using MS.Services.Identity.Domain.Filters.UserB2BFilters;

namespace MS.Services.Identity.Application.Handlers.User.Queries.B2B;

public sealed class GetB2BUsersQuery : SearchQuery<UserQueryFilter, PagedResponse<B2BUserListDTO>>
{

}

public sealed class GetB2BUsersQueryHandler : BaseQueryHandler<GetB2BUsersQuery, PagedResponse<B2BUserListDTO>>
{
    protected readonly IUserB2BService _userService;
    protected readonly IMapper _mapper;

    public GetB2BUsersQueryHandler(IUserB2BService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public override async Task<PagedResponse<B2BUserListDTO>> Handle(GetB2BUsersQuery request, CancellationToken cancellationToken  )
    {

        var searchResult = _mapper.Map<SearchQueryModel<UserB2BQueryServiceFilter>>(request);

        return await _userService.GetUsersAsync(searchResult, cancellationToken);

    }
}