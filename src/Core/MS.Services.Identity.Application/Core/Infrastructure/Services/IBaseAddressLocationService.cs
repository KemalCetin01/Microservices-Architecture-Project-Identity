using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.DTOs.AddressLocation.Request;
using MS.Services.Identity.Application.DTOs.AddressLocation.Response;
using MS.Services.Identity.Domain.Entities;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface IBaseAddressLocationService
{
    Task<AddressLocation?> GetAddressLocationByIdAsync(Guid Id, CancellationToken cancellationToken);
    Task<AddressLocation> CreateAddressLocationAsync(CreateOrUpdateAddressLocationRequestDto request, CancellationToken cancellationToken);
    AddressLocation UpdateAddressLocation(AddressLocation existingAddressLocation, CreateOrUpdateAddressLocationRequestDto request);
    Task DeleteAddressLocationAsync(Guid Id, CancellationToken cancellationToken);

}
