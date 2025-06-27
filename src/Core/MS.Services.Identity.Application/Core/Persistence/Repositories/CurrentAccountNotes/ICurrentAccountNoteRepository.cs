using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Filters.NoteFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface ICurrentAccountNoteRepository : IRepository<CurrentAccountNote>
{
    Task<SearchListModel<CurrentAccountNote>> GetAllByCurrentAccountId(Guid CurrentAccountId, SearchQueryModel<NoteQueryServiceFilter> searchQuery, CancellationToken cancellationToken);
}
