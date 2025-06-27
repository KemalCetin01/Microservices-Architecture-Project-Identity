using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IBusinessRepository : IRepository<Business>
{
    Task<Business?> GetById(Guid Id, CancellationToken cancellationToken);
    Task<bool> HasExistsBusinessName(string businessName,Guid? Id, CancellationToken cancellationToken);
    Task<ICollection<Business>> SearchFKeyBusinessesAsync(string search, CancellationToken cancellationToken);
    Task<SearchListModel<Business>> SearchAsync(SearchQueryModel<BusinessSearchFilter> searchQuery, CancellationToken cancellationToken);
}
