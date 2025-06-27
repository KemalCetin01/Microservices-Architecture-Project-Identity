using MS.Services.Core.Data.Data.Interface;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Entities;
using MS.Services.Identity.Domain.Filters.NoteFilters;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface INoteRepository : IRepository<Note>
{
    Task<Note> GetById(Guid Id, CancellationToken cancellationToken  );
    Task<SearchListModel<Domain.Entities.Note>> GetAllByRelationId(Guid relationId, SearchQueryModel<NoteQueryServiceFilter> searchQuery, CancellationToken cancellationToken);
}
