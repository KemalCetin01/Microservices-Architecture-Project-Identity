using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.User.DTOs;

namespace MS.Services.Identity.Application.Handlers.User.Queries.B2C;

public class GetB2CUserByIdQuery : IQuery<B2CUserGetByIdDTO>
{
    public Guid Id { get; set; }
}

public sealed class GetB2CUserByIdHandler : BaseQueryHandler<GetB2CUserByIdQuery, B2CUserGetByIdDTO>
{
    protected readonly IUserB2CService _userService;

    public GetB2CUserByIdHandler(IUserB2CService userService)
    {
        _userService = userService;
    }
    public override async Task<B2CUserGetByIdDTO> Handle(GetB2CUserByIdQuery request, CancellationToken cancellationToken  )
    {
        return await _userService.GetByIdAsync(request.Id, cancellationToken);
    }
}
