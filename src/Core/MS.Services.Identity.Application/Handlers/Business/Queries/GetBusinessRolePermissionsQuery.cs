using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Handlers;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Identity.Application.Handlers.B2bRoles.DTOs;
using MS.Services.Identity.Application.Helpers;

namespace MS.Services.Identity.Application.Handlers.Business.Queries;

public class GetBusinessRolePermissionsQuery : IQuery<ListResponse<GetBusinessPermissionsResponseDto>>
{
    public Guid BusinessId { get; set; }
    public Guid BusinessRoleRefId { get; set; }
}
public sealed class GetBusinessRolePermissionsQueryHandler : BaseQueryHandler<GetBusinessRolePermissionsQuery, ListResponse<GetBusinessPermissionsResponseDto>>
{
    private readonly IMapper _mapper;
    private readonly IBusinessService _businessService;
    private readonly IIdentityB2BService _identityB2Bservice;

    public GetBusinessRolePermissionsQueryHandler(IMapper mapper, IBusinessService businessService, IIdentityB2BService identityB2Bservice)
    {
        _mapper = mapper;
        _businessService = businessService;
        _identityB2Bservice = identityB2Bservice;
    }

    public override async Task<ListResponse<GetBusinessPermissionsResponseDto>> Handle(GetBusinessRolePermissionsQuery request, CancellationToken cancellationToken  )
    {
        GetBusinessResponseDto business = await _businessService.GetByIdAsync(request.BusinessId, cancellationToken);
        var result = await _identityB2Bservice.GetBusinessRolePermissionsAsync(request.BusinessRoleRefId, request.BusinessRoleRefId, cancellationToken);
        return new ListResponse<GetBusinessPermissionsResponseDto>(result);
    }
}

public sealed class GetBusinessRolePermissionsQueryValidator : AbstractValidator<GetBusinessRolePermissionsQuery>
{
    public GetBusinessRolePermissionsQueryValidator()
    {
        RuleFor(x => x.BusinessId).NotEmpty();
        RuleFor(x => x.BusinessRoleRefId).NotEmpty();
    }
}