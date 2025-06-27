using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.DTOs.Business.Response;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface IBusinessBillingAddressService : IScopedService
{
    Task<GetBusinessBillingAddressResponseDto> GetByBusinessIdAsync(Guid businessId, CancellationToken cancellationToken);
    Task<GetBusinessBillingAddressResponseDto> CreateOrUpdateAsync(CreateOrUpdateBusinessBillingAddressRequestDto request, CancellationToken cancellationToken);

}
