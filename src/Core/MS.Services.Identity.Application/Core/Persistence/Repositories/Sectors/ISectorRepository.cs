using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface ISectorRepository : IRepository<Sector>
{
    Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken);
    Task<SearchListModel<Sector>> SearchAsync(SearchQueryModel<SearchSectorsQueryFilterModel> searchQuery, CancellationToken cancellationToken);

    Task<bool> HasSectorExists(string name,int? id, CancellationToken cancellationToken  );
}
