

using AutoMapper;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.User.DTOs;
using MS.Services.Identity.Application.Handlers.User.Queries.Filters;
using MS.Services.Identity.Domain.Filters.UserB2CFilters;

namespace MS.Services.Identity.Application.Handlers.User.Queries.B2C;

public class GetB2CUsersQuery : SearchQuery<UserQueryFilter, PagedResponse<B2CUserListDTO>>
{

}

public sealed class GetB2CUsersQueryHandler : BaseQueryHandler<GetB2CUsersQuery, PagedResponse<B2CUserListDTO>>
{
    protected readonly IUserB2CService _userService;
    protected readonly IMapper _mapper;

    public GetB2CUsersQueryHandler(IUserB2CService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    public override async Task<PagedResponse<B2CUserListDTO>> Handle(GetB2CUsersQuery request, CancellationToken cancellationToken  )
    {
        var searchResult = _mapper.Map<SearchQueryModel<UserB2CQueryServiceFilter>>(request);

        return await _userService.GetUsersAsync(searchResult, cancellationToken);
    }
}