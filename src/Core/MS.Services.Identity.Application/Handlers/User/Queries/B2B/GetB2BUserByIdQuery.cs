using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Handlers.User.DTOs;


namespace MS.Services.Identity.Application.Handlers.User.Queries.B2B;

public class GetB2BUserByIdQuery : IQuery<B2BUserGetByIdDTO>
{
    public Guid Id { get; set; }
}

public sealed class GetB2BUserByIdHandler : BaseQueryHandler<GetB2BUserByIdQuery, B2BUserGetByIdDTO>
{
    protected readonly IUserB2BService _userService;

    public GetB2BUserByIdHandler(IUserB2BService userService)
    {
        _userService = userService;
    }
    public override async Task<B2BUserGetByIdDTO> Handle(GetB2BUserByIdQuery request, CancellationToken cancellationToken  )
    {
        return await _userService.GetByIdAsync(request.Id, cancellationToken);
    }
}
