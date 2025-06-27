using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Identity.Application.Helpers;

using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Application.Handlers.User.Queries.B2B;

public sealed class GetBusinessRolesQuery : IQuery<ListResponse<GetBusinessRoleResponseDto>>
{
    public Guid BusinessId { get; set; }
}
public sealed class GetBusinessRolesQueryHandler : BaseQueryHandler<GetBusinessRolesQuery, ListResponse<GetBusinessRoleResponseDto>>
{
    private readonly IIdentityB2BService _identityB2BService;
    private readonly IMapper _mapper;
    private readonly IBusinessService _businessService;

    public GetBusinessRolesQueryHandler(IBusinessService businessService, IMapper mapper, IIdentityB2BService identityB2BService)
    {
        _mapper = mapper;
        _businessService = businessService;
        _identityB2BService = identityB2BService;
    }

    public override async Task<ListResponse<GetBusinessRoleResponseDto>> Handle(GetBusinessRolesQuery request, CancellationToken cancellationToken)
    {
        GetBusinessResponseDto business = await _businessService.GetByIdAsync(request.BusinessId, cancellationToken);
        if (business.IdentityRefId == null) throw new ResourceNotFoundException("Identity server business reference id is null");
        var result = await _identityB2BService.GetBusinessRolesAsync(business.IdentityRefId.Value, cancellationToken);
        return new ListResponse<GetBusinessRoleResponseDto>(result);
    }
}

public sealed class GetBusinessRolesQueryValidator : AbstractValidator<GetBusinessRolesQuery>
{
    public GetBusinessRolesQueryValidator()
    {
        RuleFor(x => x.BusinessId).NotEmpty();
    }

}