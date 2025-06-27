using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.DTOs.Business.Response;

namespace MS.Services.Identity.Application.Handlers.Business.Queries;

public sealed class GetBusinessBillingAddressByBusinessIdQuery : IQuery<GetBusinessBillingAddressResponseDto>
{
    public Guid Id { get; set; }
}

public sealed class GetBusinessBillingAddressByBusinessIdQueryHandler : BaseQueryHandler<GetBusinessBillingAddressByBusinessIdQuery, GetBusinessBillingAddressResponseDto>
{
    private readonly IBusinessBillingAddressService _businessBillingAddressService;

    public GetBusinessBillingAddressByBusinessIdQueryHandler(IBusinessBillingAddressService businessBillingAddressService)
    {
        _businessBillingAddressService = businessBillingAddressService;
    }
    public override async Task<GetBusinessBillingAddressResponseDto> Handle(GetBusinessBillingAddressByBusinessIdQuery request, CancellationToken cancellationToken  )
    {
        return await _businessBillingAddressService.GetByBusinessIdAsync(request.Id, cancellationToken);
    }
}
