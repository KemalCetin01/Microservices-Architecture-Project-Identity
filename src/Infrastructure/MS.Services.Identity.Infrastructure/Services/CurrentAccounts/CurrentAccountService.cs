using AutoMapper;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Core.ExceptionHandling.Exceptions;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.UoW;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.Commands;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.DTOs;
using MS.Services.Identity.Application.Helpers.Utility;
using MS.Services.Identity.Application.Messages;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Exceptions;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Infrastructure.Services.CurrentAccounts
{
    public class CurrentAccountService : ICurrentAccountService
    {
        private readonly ICurrentAccountRepository _currentAccountRepository;
        private readonly IIdentityUnitOfWork _identityUnitOfWork;
        private readonly IMapper _mapper;
        public CurrentAccountService(IMapper mapper, ICurrentAccountRepository currentAccountRepository, IIdentityUnitOfWork identityUnitOfWork)
        {
            _mapper = mapper;
            _currentAccountRepository = currentAccountRepository;
            _identityUnitOfWork = identityUnitOfWork;
        }


        public async Task<ICollection<CurrentAccountDTO>> GetAsync(int skip, int take, CancellationToken cancellationToken)
        {
            var result = await _currentAccountRepository.GetAsync(skip, take, cancellationToken);
            var protectedName = result.Select(x => x.FirstName).FirstOrDefault();
            return _mapper.Map<ICollection<CurrentAccountDTO>>(result);

        }

        public async Task<ICollection<CurrentAccountDTO>> GetAllAsync(string search, CancellationToken cancellationToken)
        {
            var result = await _currentAccountRepository.GetAllAsync(search, cancellationToken);
            return _mapper.Map<ICollection<CurrentAccountDTO>>(result);

        }

        public async Task<CurrentAccountDTO> GetById(Guid Id, CancellationToken cancellationToken)
        {
            var result = await _currentAccountRepository.GetById(Id, cancellationToken);
            // var mapToLang = await _localizationMapper.MapTo<CurrentAccount>(result, "fr"); // NOT: burdaki fr Locale'den gelecek

            return _mapper.Map<CurrentAccountDTO>(result);
        }

        public async Task<PagedResponse<GeneralCurrentAccountsDTO>> GetGeneralCurrentAsync(SearchQueryModel<GetAllGeneralCurrentAccountsQueryFilterModel> searchQuery, CancellationToken cancellationToken)
        {
            var result = await _currentAccountRepository.GetGeneralCurrentAsync(searchQuery, cancellationToken);
            return _mapper.Map<PagedResponse<GeneralCurrentAccountsDTO>>(result);
        }

        public async Task<bool> Remove(Guid Id, CancellationToken cancellationToken)
        {
            var existCurrentAccount = await _currentAccountRepository.GetById(Id, cancellationToken);

            if (existCurrentAccount == null)
                throw new ValidationException(UserStatusCodes.UserNotFound.Message, UserStatusCodes.UserNotFound.StatusCode);

            existCurrentAccount.IsDeleted = true;
            _currentAccountRepository.Update(existCurrentAccount);
            await _identityUnitOfWork.CommitAsync(cancellationToken);
            var result = await _identityUnitOfWork.CommitAsync(cancellationToken);

            return result == 1 ? true : false;
        }

        public async Task<CurrentAccountDTO> Update(UpdateCurrentAccountCommand model, CancellationToken cancellationToken)
        {
            var currentAccount = await _currentAccountRepository.GetById(model.Id, cancellationToken);
            if (currentAccount == null)
                throw new ResourceNotFoundException(CurrentAccountsConstants.CurrentAccountsNotExists);

            currentAccount.SiteStatus = model.SiteStatus;
            currentAccount.SalesAndDistribution = model.SalesAndDistribution;
            _currentAccountRepository.Update(currentAccount);

            await _identityUnitOfWork.CommitAsync(cancellationToken);

            return _mapper.Map<CurrentAccountDTO>(currentAccount);
        }
        public async Task<CurrentAccountDTO> AddAsync(CreateCurrentAccountCommand model, CancellationToken cancellationToken)
        {
            //var existsCurrentAccount = await _currentAccountRepository.ExistsCurrentAccountAsync(model.CompanyName, cancellationToken);

            //if (existsCurrentAccount)
            //    throw new ValidationException(UserStatusCodes.CurrentAccountConflict.Message, UserStatusCodes.CurrentAccountConflict.StatusCode);

            var currentAccountEntity = _mapper.Map<CurrentAccount>(model);
            await _currentAccountRepository.AddAsync(currentAccountEntity, cancellationToken);

            await _identityUnitOfWork.CommitAsync(cancellationToken);

            return _mapper.Map<CurrentAccountDTO>(currentAccountEntity);
        }

        public async Task<long> GetCurrentAccountsCount() =>
        await _currentAccountRepository.GetCurrentAccountsCount();

        public async Task CreateOrUpdateFromMessageAsync(LogoErpCurrentAccountMessage request, CancellationToken cancellationToken = default)
        {
            var erpRefId = request.LogicalRef.ToString();
            var currentAccount = await _currentAccountRepository.GetDetailByErpRefIdAsync(erpRefId, cancellationToken);

            if (currentAccount == null)
            {
                currentAccount = _mapper.Map<CurrentAccount>(request);
                await _currentAccountRepository.AddAsync(currentAccount, cancellationToken);
            }
            else
            {
                currentAccount.ErpRefId = request.LogicalRef.ToString();
                currentAccount.Code = request.Code;
                currentAccount.FirstName = request.FirstName;
                currentAccount.LastName = request.LastName;
                currentAccount.CurrentAccountName = request.CurrentAccountName;
                currentAccount.IdentityNumber = request.IdentityNumber;
                currentAccount.TaxNo = request.TaxNo;
                currentAccount.CurrentCreateLogoDate = request.CurrentCreateLogoDate.SetKindUtc();
                currentAccount.CurrentAccountType = request.CurrentAccountType;
                currentAccount.CurrentAccountStatus = request.CurrentAccountStatus;
                currentAccount.SalesAndDistribution = request.SalesAndDistribution;
                currentAccount.IsForeign = request.IsForeign;
                currentAccount.TransactionCurrency = request.TransactionCurrency;
                currentAccount.ExchangeRate = request.ExchangeRate;
                currentAccount.CompanyType = request.CompanyType;
                currentAccount.Maturity = request.Maturity;
                currentAccount.SiteStatus = currentAccount.SiteStatus;
                currentAccount.IsSaleExchangeRate = currentAccount.IsSaleExchangeRate;

                _currentAccountRepository.Update(currentAccount);
            }
            await _identityUnitOfWork.CommitAsync(cancellationToken);

        }
    }
}
