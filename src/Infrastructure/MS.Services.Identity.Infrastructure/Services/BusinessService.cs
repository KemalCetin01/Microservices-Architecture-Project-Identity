using AutoMapper;
using Microsoft.Extensions.Options;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Helpers;
using MS.Services.Identity.Domain.Enums;
using MS.Services.Identity.Domain.Exceptions;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Core.Base.Dtos.Response;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Application.Core.Infrastructure.External;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.DTOs.Identity.Request;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Application.Extensions;
using MS.Services.Identity.Infrastructure.Validators;

namespace MS.Services.Identity.Infrastructure.Services;
public class BusinessService : IBusinessService
{
    private readonly IMapper _mapper;
    private readonly IBusinessRepository _businessRepository;
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly IIdentityIdSequenceRepository _identityIdSequenceRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityB2BService _identityB2BService;
    private readonly IBusinessValidator _businessValidator;
    public BusinessService(IMapper mapper,
                           IBusinessRepository businessRepository,
                           IIdentityUnitOfWork identityUnitOfWork,
                           ICurrentUserService currentUserService,
                           IIdentityIdSequenceRepository identityIdSequenceRepository,
                           IIdentityB2BService identityB2BService,
                           IBusinessValidator businessValidator)
    {
        _mapper = mapper;
        _businessRepository = businessRepository;
        _identityUnitOfWork = identityUnitOfWork;
        _currentUserService = currentUserService;
        _identityIdSequenceRepository = identityIdSequenceRepository;
        _identityB2BService = identityB2BService;
        _businessValidator = businessValidator;
    }
    private async Task<string> GenerateBusinessCode(string entityName, CancellationToken cancellationToken)
    {
        var businessKey = await _identityIdSequenceRepository.GetAndUpdateByEntity(entityName);
        if (businessKey != null)
            return businessKey.Prefix + "-" + businessKey.Counter;
        else throw new ApiException("Can not generate business code");
    }
    
    public async Task<ListResponse<LabelValueResponse>> GetFKeyBusinessesAsync(string search, CancellationToken cancellationToken = default)
    {
        var result = await _businessRepository.SearchFKeyBusinessesAsync(search, cancellationToken);
        return _mapper.Map<ListResponse<LabelValueResponse>>(result);

    }

    public async Task<GetBusinessResponseDto> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        var result = await _businessRepository.GetById(Id, cancellationToken) 
            ?? throw new ResourceNotFoundException("Business is not found");;
        return _mapper.Map<GetBusinessResponseDto>(result);
    }

    public async Task<GetBusinessResponseDto> CreateAsync(CreateBusinessRequestDTO request, CancellationToken cancellationToken)
    {
        await _businessValidator.ValidateBusinessBeforeSave(request, cancellationToken);

        var businessCode = await GenerateBusinessCode(nameof(Business), cancellationToken);

        var identityResponse = await _identityB2BService.CreateBusinessAsync(new CreateIdentityBusinessRequestDto(businessCode), cancellationToken);

        if (!identityResponse.IsSuccess)
            throw new ApiException("Business can not be created at identity server");

        var businessEntity = request.CreateBusiness(businessCode, identityResponse.IdentityRefId);
        await _businessRepository.AddAsync(businessEntity, cancellationToken);
        await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<GetBusinessResponseDto>(businessEntity);
    }

    public async Task<PagedResponse<SearchBusinessResponseDto>> SearchAsync(SearchQueryModel<BusinessSearchFilter> searchQuery, CancellationToken cancellationToken)
    {
        var businesses = await _businessRepository.SearchAsync(searchQuery, cancellationToken);

        return _mapper.Map<PagedResponse<SearchBusinessResponseDto>>(businesses);
    }

    public async Task DeleteAsync(Guid Id, CancellationToken cancellationToken)
    {
        var existBusiness = await _businessRepository.GetById(Id, cancellationToken);
        if (existBusiness == null)
            throw new ApiException(UserStatusCodes.BusinessNotFound.Message, UserStatusCodes.BusinessNotFound.StatusCode);
        
        if (existBusiness.IdentityRefId.HasValue)
            await _identityB2BService.DeleteBusinessAsync(existBusiness.IdentityRefId.Value, cancellationToken);
        //TODO retry mechanisms

        if (existBusiness.BillingAddress != null)
            existBusiness.BillingAddress.IsDeleted = true;

        existBusiness.IsDeleted = true;
        _businessRepository.Update(existBusiness);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
    }

    public async Task<GetBusinessResponseDto> UpdateAsync(UpdateBusinessRequestDto request, CancellationToken cancellationToken)
    {
        var currentBusiness = await _businessRepository.GetById(request.Id, cancellationToken) 
            ?? throw new ResourceNotFoundException("Business is not found");

        await _businessValidator.ValidateBusinessBeforeSave(request, cancellationToken);

        currentBusiness.Name = request.Name;
        currentBusiness.RepresentativeId = request.RepresentativeId;
        currentBusiness.BusinessStatusId = (int) request.BusinessStatus;

        currentBusiness.SectorId = request.SectorId;
        currentBusiness.ActivityAreaId = request.ActivityAreaId;
        currentBusiness.NumberOfEmployeeId = request.NumberOfEmployeeId;
        currentBusiness.DiscountRate = request.DiscountRate;
        currentBusiness.PhoneCountryCode = request.PhoneCountryCode;
        currentBusiness.Phone = request.Phone;
        currentBusiness.FaxNumber = request.FaxNumber;

        _businessRepository.Update(currentBusiness);
        var result = await _identityUnitOfWork.CommitAsync(cancellationToken);

        return _mapper.Map<GetBusinessResponseDto>(currentBusiness);

    }

    public async Task<UpdateBusinessReviewStatusResponseDto> UpdateReviewStatusAsync(UpdateBusinessReviewStatusRequestDto model, CancellationToken cancellationToken)
    {
        var currentBusiness = await _businessRepository.GetById(model.Id, cancellationToken);
        if (currentBusiness == null)
            throw new ApiException(UserStatusCodes.BusinessNotFound.Message, UserStatusCodes.BusinessNotFound.StatusCode);
        currentBusiness.ReviewStatus = model.ReviewStatus;
        currentBusiness.ReviewDate = DateTime.UtcNow;
        currentBusiness.ReviewBy = _currentUserService.UserId;

        _businessRepository.Update(currentBusiness);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
        return _mapper.Map<UpdateBusinessReviewStatusResponseDto>(currentBusiness);
    }

   
}