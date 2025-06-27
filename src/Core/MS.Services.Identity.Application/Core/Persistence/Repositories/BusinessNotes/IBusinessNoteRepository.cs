using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Filters.NoteFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IBusinessNoteRepository : IRepository<BusinessNote>
{
    Task<SearchListModel<BusinessNote>> GetAllByBusinessId(Guid BusinessId, SearchQueryModel<NoteQueryServiceFilter> searchQuery, CancellationToken cancellationToken);
}
