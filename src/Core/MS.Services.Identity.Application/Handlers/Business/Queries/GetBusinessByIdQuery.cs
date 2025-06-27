using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.DTOs.Business.Response;

namespace MS.Services.Identity.Application.Handlers.Business.Queries;

public sealed class GetBusinessByIdQuery : IQuery<GetBusinessResponseDto>
{
    public Guid Id { get; set; }
}

public sealed class GetBusinessByIdHandler : BaseQueryHandler<GetBusinessByIdQuery, GetBusinessResponseDto>
{
    private readonly IBusinessService _businessService;

    public GetBusinessByIdHandler(IBusinessService businessService)
    {
        _businessService = businessService;
    }
    public override async Task<GetBusinessResponseDto> Handle(GetBusinessByIdQuery request, CancellationToken cancellationToken  )
    {
        return await _businessService.GetByIdAsync(request.Id, cancellationToken);
    }
}
