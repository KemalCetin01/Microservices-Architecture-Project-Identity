using FluentValidation;
using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.DTOs.Business.Response;

namespace MS.Services.Identity.Application.Handlers.Business.Queries;

public sealed class GetBusinessRoleByIdQuery : IQuery<GetBusinessRoleResponseDto>
{
    public Guid RoleRefId { get; set; }
}

public sealed class GetBusinessRoleByIdQueryHandler : BaseQueryHandler<GetBusinessRoleByIdQuery, GetBusinessRoleResponseDto>
{
    private readonly IIdentityB2BService _identityB2BService;

    public GetBusinessRoleByIdQueryHandler(IIdentityB2BService identityB2BService)
    {
        _identityB2BService = identityB2BService;
    }
    public override async Task<GetBusinessRoleResponseDto> Handle(GetBusinessRoleByIdQuery request, CancellationToken cancellationToken  )
    {
        return await _identityB2BService.GetBusinessRoleByIdAsync(request.RoleRefId, cancellationToken);
    }
}

public sealed class GetBusinessRoleByIdQueryValidator : AbstractValidator<GetBusinessRoleByIdQuery>
{
    public GetBusinessRoleByIdQueryValidator()
    {
        RuleFor(x => x.RoleRefId).NotEmpty();
    }
}