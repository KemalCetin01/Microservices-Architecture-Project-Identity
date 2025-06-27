using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IPositionRepository : IRepository<Position>
{
    Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken  );
    Task<SearchListModel<Position>> SearchAsync(SearchQueryModel<SearchPositionsQueryFilterModel> searchQuery, CancellationToken cancellationToken);

    Task<bool> HasPositionExists(string name,int? id, CancellationToken cancellationToken  );
}
