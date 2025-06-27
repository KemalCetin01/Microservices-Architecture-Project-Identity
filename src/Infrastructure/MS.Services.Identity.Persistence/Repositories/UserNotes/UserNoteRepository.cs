using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Domain.Filters.NoteFilters;
using MS.Services.Identity.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Persistence.Repositories;

public class UserNoteRepository : Repository<UserNote, IdentityDbContext>, IUserNoteRepository
{
    public UserNoteRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<SearchListModel<UserNote>> GetAllByUserId(Guid UserId, SearchQueryModel<NoteQueryServiceFilter> searchQuery, CancellationToken cancellationToken)
    {
        var q = Queryable().Include(x => x.Note.CreatedByUser.User).Where(x => x.UserId == UserId && x.Note.IsDeleted == false)
            .OrderByDescending(x => x.Note.CreatedDate);

        return await SearchAsync(q, searchQuery);
    }
}
