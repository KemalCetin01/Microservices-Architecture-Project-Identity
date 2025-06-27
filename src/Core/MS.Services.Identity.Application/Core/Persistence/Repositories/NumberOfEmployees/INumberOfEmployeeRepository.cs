using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface INumberOfEmployeeRepository : IRepository<NumberOfEmployee>
{
    Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken);
    Task<SearchListModel<NumberOfEmployee>> SearchAsync(SearchQueryModel<SearchNumberOfEmployeeQueryFilterModel> searchQuery, CancellationToken cancellationToken);

    Task<bool> HasNumberOfEmployeeExists(string name, int? id, CancellationToken cancellationToken);
}
