using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Domain.Filters.NoteFilters;
using MS.Services.Identity.Persistence.Context;

namespace MS.Services.Identity.Persistence.Repositories;

public class NotesRepository : Repository<Note, IdentityDbContext>, INoteRepository
{
    public NotesRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Note> GetById(Guid Id, CancellationToken cancellationToken  )
    {
        return await Queryable().Where(x => x.Id == Id).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SearchListModel<Note>> GetAllByRelationId(Guid relationId, SearchQueryModel<NoteQueryServiceFilter> searchQuery, CancellationToken cancellationToken)
    {
        var q = Queryable().Where(x=>x.IsDeleted == false).Include(x => x.CreatedByUser)
            .OrderByDescending(x => x.CreatedDate);

        return await SearchAsync(q, searchQuery);
    }
}
