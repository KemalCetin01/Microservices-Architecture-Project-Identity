using AutoMapper;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Infrastructure.Services.CurrentAccountBusinesses;

public class BusinessCurrentAccountService : IBusinessCurrentAccountService
{
    private readonly IIdentityUnitOfWork _identityUnitOfWork;
    private readonly ICurrentAccountRepository _currentAccountRepository;
    private readonly IBusinessRepository _businessRepository;
    private readonly IMapper _mapper;

    public BusinessCurrentAccountService(IIdentityUnitOfWork identityUnitOfWork, ICurrentAccountRepository currentAccountRepository, IMapper mapper, IBusinessRepository businessRepository)
    {
        _identityUnitOfWork = identityUnitOfWork;
        _currentAccountRepository = currentAccountRepository;
        _mapper = mapper;
        _businessRepository = businessRepository;
    }

    public async Task<PagedResponse<SearchBusinessCurrentAccountsResponseDto>> SearchAsync(SearchQueryModel<BusinessCurrentAccountSearchFilter> searchQuery, CancellationToken cancellationToken)
    {
        var result = await _currentAccountRepository.SearchBusinessCurrentAccountsAsync(searchQuery, cancellationToken);
        return _mapper.Map<PagedResponse<SearchBusinessCurrentAccountsResponseDto>>(result);
    }

     public async Task AddCurrentAccountToBusinessAsync(Guid businessId, Guid currentAccountId, CancellationToken cancellationToken = default)
    {
        Business? business = await _businessRepository.GetById(businessId);
        if (business == null) throw new ResourceNotFoundException("Business is not found");
        CurrentAccount? currentAccount = await _currentAccountRepository.GetById(currentAccountId);
        if (currentAccount == null) throw new ResourceNotFoundException("Current Account is not found");
        if (currentAccount.BusinessId != null) {
            throw new ApiException($"This current account is already connected to a business, BusinessId:{currentAccount.BusinessId}");
        }
        currentAccount.BusinessId = businessId;
        _currentAccountRepository.Update(currentAccount);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
    }

    public async Task RemoveCurrentAccountFromBusinessAsync(Guid businessId, Guid currentAccountId, CancellationToken cancellationToken = default)
    {
        Business? business = await _businessRepository.GetById(businessId);
        if (business == null) throw new ResourceNotFoundException("Business is not found");
        CurrentAccount? currentAccount = await _currentAccountRepository.GetById(currentAccountId);
        if (currentAccount == null) throw new ResourceNotFoundException("Current Account is not found");
        
        currentAccount.BusinessId = null;
        _currentAccountRepository.Update(currentAccount);
        await _identityUnitOfWork.CommitAsync(cancellationToken);
    }

}
