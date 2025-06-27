
using AutoMapper;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.DTOs.AddressLocation.Request;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Exceptions;

namespace MS.Services.Identity.Infrastructure.Services;

public class BusinessBillingAddressService : BaseAddressLocationService, IBusinessBillingAddressService
{
//    private readonly IMapper _mapper;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IBusinessBillingAddressRepository _businessBillingAddressRepository;
    private readonly IBusinessRepository _businessRepository;

    public BusinessBillingAddressService(IMapper mapper, 
            IIdentityUnitOfWork identityUnitOfWork, 
            IBusinessBillingAddressRepository businessBillingAddressRepository, 
            IAddressLocationRepository addressLocationRepository, 
            IBusinessRepository businessRepository) : base(mapper, addressLocationRepository)
    {
        //_mapper = mapper;
        _identityUnitOfWork = identityUnitOfWork;
        _businessBillingAddressRepository = businessBillingAddressRepository;
        _businessRepository = businessRepository;
    }

    public async Task<GetBusinessBillingAddressResponseDto> GetByBusinessIdAsync(Guid Id, CancellationToken cancellationToken  )
    {
        var result = await _businessBillingAddressRepository.GetByBusinessId(Id, cancellationToken);
        return _mapper.Map<GetBusinessBillingAddressResponseDto>(result);
    }

    public async Task DeleteAsync(Guid Id, CancellationToken cancellationToken  )
    {
        var existingAddress = await _businessBillingAddressRepository.GetById(Id, cancellationToken);
        if (existingAddress == null) throw new ValidationException("Business Billing Address is not found!");
        
        existingAddress.IsDeleted = true;
        _businessBillingAddressRepository.Update(existingAddress);
        await _identityUnitOfWork.CommitAsync(cancellationToken);

    }

    public async Task<GetBusinessBillingAddressResponseDto> CreateOrUpdateAsync(CreateOrUpdateBusinessBillingAddressRequestDto request, CancellationToken cancellationToken  )
    {
        if (!request.BusinessId.HasValue) throw new ValidationException("Business Id can not be Empty");
        var existingAddress = await _businessBillingAddressRepository.GetByBusinessId(request.BusinessId.Value, cancellationToken);
        if (existingAddress == null) {
            return await CreateBusinessBillingAddressAsync(request, cancellationToken);
        }

        return await UpdateBusinessBillingAddressAsync(request, existingAddress, cancellationToken);
    }

    private async Task<GetBusinessBillingAddressResponseDto> CreateBusinessBillingAddressAsync(CreateOrUpdateBusinessBillingAddressRequestDto request, CancellationToken cancellationToken) 
    {
        if (!request.BusinessId.HasValue) throw new ValidationException("Business Id can not be Empty");
        AddressLocation addressLocation = await CreateAddressLocationAsync(request.AddressLocation, cancellationToken);

        BusinessBillingAddress billingAddress = new BusinessBillingAddress()
        {
            BusinessId = request.BusinessId.Value,
            AddressLocationId = addressLocation.Id,
            BillingType = request.BillingType,
            FirstName = request.FirstName,
            LastName = request.LastName,
            CompanyName = request.CompanyName,
            CompanyType = request.CompanyType,
            IdentityNumber = request.IdentityNumber,
            TaxNumber = request.TaxNumber,
            TaxOffice = request.TaxOffice,
            PhoneNumber = request.PhoneNumber,
            Email = request.Email
        };

        await _businessBillingAddressRepository.AddAsync(billingAddress, cancellationToken);
        var result = await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<GetBusinessBillingAddressResponseDto>(billingAddress);
    }

    private async Task<GetBusinessBillingAddressResponseDto> UpdateBusinessBillingAddressAsync(CreateOrUpdateBusinessBillingAddressRequestDto request, BusinessBillingAddress businessBillingAddress, CancellationToken cancellationToken)
    {
        businessBillingAddress.BillingType = request.BillingType;
        businessBillingAddress.FirstName = request.FirstName;
        businessBillingAddress.LastName = request.LastName;
        businessBillingAddress.CompanyName = request.CompanyName;
        businessBillingAddress.CompanyType = request.CompanyType;
        businessBillingAddress.IdentityNumber = request.IdentityNumber;
        businessBillingAddress.TaxNumber = request.TaxNumber;
        businessBillingAddress.TaxOffice = request.TaxOffice;
        businessBillingAddress.PhoneNumber = request.PhoneNumber;
        businessBillingAddress.Email = request.Email;

        if (businessBillingAddress.AddressLocation == null)
        {
            AddressLocation addressLocation = await CreateAddressLocationAsync(request.AddressLocation, cancellationToken);
            businessBillingAddress.AddressLocationId = addressLocation.Id;
        } else {
            UpdateAddressLocation(businessBillingAddress.AddressLocation, request.AddressLocation);
        }

        _businessBillingAddressRepository.Update(businessBillingAddress);
        var result = await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<GetBusinessBillingAddressResponseDto>(businessBillingAddress);
    }
}
