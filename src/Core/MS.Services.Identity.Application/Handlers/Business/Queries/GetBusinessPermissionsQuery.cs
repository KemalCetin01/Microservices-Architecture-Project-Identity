using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Business.Response;

namespace MS.Services.Identity.Application.Handlers.Business.Queries;

public sealed class GetBusinessPermissionsQuery : IQuery<ListResponse<GetBusinessPermissionsResponseDto>>
{
    public Guid BusinessId { get; set; }
}
public sealed class GetBusinessPermissionsQueryHandler : BaseQueryHandler<GetBusinessPermissionsQuery, ListResponse<GetBusinessPermissionsResponseDto>>
{
    private readonly IIdentityB2BService _identityB2BService;
    private readonly IMapper _mapper;
    private readonly IBusinessService _businessService;

    public GetBusinessPermissionsQueryHandler(IBusinessService businessService, IMapper mapper, IIdentityB2BService identityB2BService)
    {
        _mapper = mapper;
        _businessService = businessService;
        _identityB2BService = identityB2BService;
    }

    public override async Task<ListResponse<GetBusinessPermissionsResponseDto>> Handle(GetBusinessPermissionsQuery request, CancellationToken cancellationToken)
    {
        GetBusinessResponseDto business = await _businessService.GetByIdAsync(request.BusinessId, cancellationToken);
        if (business.IdentityRefId == null) throw new ResourceNotFoundException("Identity server business reference id is null");
        var result = await _identityB2BService.GetBusinessPermissionsAsync(business.IdentityRefId!.Value, cancellationToken);
        return new ListResponse<GetBusinessPermissionsResponseDto>(result);
    }
}

public sealed class GetBusinessPermissionsQueryValidator : AbstractValidator<GetBusinessPermissionsQuery>
{
    public GetBusinessPermissionsQueryValidator()
    {
        RuleFor(x => x.BusinessId).NotEmpty();
    }
}