using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.Commands;
using MS.Services.Identity.Application.Handlers.CurrentAccounts.DTOs;
using MS.Services.Identity.Application.Messages;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;
public interface ICurrentAccountService : IScopedService
{
    Task<ICollection<CurrentAccountDTO>> GetAsync(int skip, int take, CancellationToken cancellationToken  );
    Task<ICollection<CurrentAccountDTO>> GetAllAsync(string search, CancellationToken cancellationToken  );
    Task<CurrentAccountDTO> GetById(Guid Id, CancellationToken cancellationToken  );
    Task<PagedResponse<GeneralCurrentAccountsDTO>> GetGeneralCurrentAsync(SearchQueryModel<GetAllGeneralCurrentAccountsQueryFilterModel> searchQuery, CancellationToken cancellationToken  );
    Task<CurrentAccountDTO> AddAsync(CreateCurrentAccountCommand model, CancellationToken cancellationToken  );
    Task<CurrentAccountDTO> Update(UpdateCurrentAccountCommand model, CancellationToken cancellationToken  );
    Task<bool> Remove(Guid Id, CancellationToken cancellationToken  );
    Task<long> GetCurrentAccountsCount();
    Task CreateOrUpdateFromMessageAsync(LogoErpCurrentAccountMessage request, CancellationToken cancellationToken = default);
}
