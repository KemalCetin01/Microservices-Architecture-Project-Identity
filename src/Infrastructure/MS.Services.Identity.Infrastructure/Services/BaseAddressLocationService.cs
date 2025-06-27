
using AutoMapper;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.DTOs.AddressLocation.Request;
using MS.Services.Identity.Application.DTOs.AddressLocation.Response;
using MS.Services.Identity.Domain.Entities;

namespace MS.Services.Identity.Infrastructure.Services;

public abstract class BaseAddressLocationService : IBaseAddressLocationService
{
    protected readonly IMapper _mapper;
    protected readonly IAddressLocationRepository _addressLocationRepository;

    public BaseAddressLocationService(IMapper mapper, IAddressLocationRepository addressLocationRepository)
    {
        _mapper = mapper;
        _addressLocationRepository = addressLocationRepository;
    }
    public async Task<AddressLocation?> GetAddressLocationByIdAsync(Guid Id, CancellationToken cancellationToken  )
    {
        return await _addressLocationRepository.GetById(Id, cancellationToken);
    }

    public async Task<AddressLocation> CreateAddressLocationAsync(CreateOrUpdateAddressLocationRequestDto request, CancellationToken cancellationToken)
    {
        AddressLocation addressLocation = new AddressLocation()
        {
            CountryCode = request.CountryCode,
            CityId = request.CityId,
            CityName = request.CityName,
            TownId = request.TownId,
            TownName = request.TownName,
            DistrictId = request.DistrictId,
            DistrictName = request.DistrictName,
            AddressLine1 = request.AddressLine1,
            AddressLine2 = request.AddressLine2,
            ZipCode = request.ZipCode,
            AddressDescription = request.AddressDescription
        };

        await _addressLocationRepository.AddAsync(addressLocation, cancellationToken);

        return addressLocation;

    }

    public AddressLocation UpdateAddressLocation(AddressLocation existingAddressLocation, CreateOrUpdateAddressLocationRequestDto request)
    {

        if (existingAddressLocation == null) throw new ValidationException("Address Location can not be empty!");

        existingAddressLocation.CountryCode = request.CountryCode;
        existingAddressLocation.CityId = request.CityId;
        existingAddressLocation.CityName = request.CityName;
        existingAddressLocation.TownId = request.TownId;
        existingAddressLocation.TownName = request.TownName;
        existingAddressLocation.DistrictId = request.DistrictId;
        existingAddressLocation.DistrictName = request.DistrictName;
        existingAddressLocation.ZipCode = request.ZipCode;
        existingAddressLocation.AddressLine1 = request.AddressLine1;
        existingAddressLocation.AddressLine2 = request.AddressLine2;
        existingAddressLocation.AddressDescription = request.AddressDescription;

        _addressLocationRepository.Update(existingAddressLocation);

        return existingAddressLocation;

    }

    public async Task DeleteAddressLocationAsync(Guid Id, CancellationToken cancellationToken)
    {
        var existingAddressLocation = await _addressLocationRepository.GetById(Id, cancellationToken);
        if (existingAddressLocation == null) throw new ValidationException("Address Locations is not found!");

        existingAddressLocation.IsDeleted = true;
        _addressLocationRepository.Update(existingAddressLocation);
    }
}
