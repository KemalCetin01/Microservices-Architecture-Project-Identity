using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.EntityFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IOccupationRepository : IRepository<Occupation>
{
    Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken);
    Task<SearchListModel<Occupation>> SearchAsync(SearchQueryModel<SearchOccupationsQueryFilterModel> searchQuery, CancellationToken cancellationToken);

    Task<bool> HasOccupationExists(string name,int? id, CancellationToken cancellationToken  );
}
