using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IActivityAreaRepository : IRepository<ActivityArea>
{
    Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken  );
    Task<SearchListModel<ActivityArea>> SearchAsync(SearchQueryModel<SearchActivityAreasQueryFilterModel> searchQuery, CancellationToken cancellationToken);

    Task<bool> HasActivityAreaExists(string name,int? id, CancellationToken cancellationToken  );
}
