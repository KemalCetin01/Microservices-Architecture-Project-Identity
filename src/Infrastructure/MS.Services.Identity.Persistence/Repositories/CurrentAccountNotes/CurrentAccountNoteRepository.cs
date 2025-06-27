using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Domain.Filters.NoteFilters;
using MS.Services.Identity.Persistence.Context;

namespace MS.Services.Identity.Persistence.Repositories;

public class CurrentAccountNoteRepository : Repository<CurrentAccountNote, IdentityDbContext>, ICurrentAccountNoteRepository
{
    public CurrentAccountNoteRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<SearchListModel<CurrentAccountNote>> GetAllByCurrentAccountId(Guid CurrentAccountId, SearchQueryModel<NoteQueryServiceFilter> searchQuery, CancellationToken cancellationToken)
    {
        var q = Queryable().Include(x => x.Note.CreatedByUser.User).Where(x => x.CurrentAccountId == CurrentAccountId && x.Note!.IsDeleted == false)
            .OrderByDescending(x => x.Note!.CreatedDate);

        return await SearchAsync(q, searchQuery);
    }
}
