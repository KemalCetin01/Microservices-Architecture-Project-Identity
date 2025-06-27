using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Persistence.Context;

namespace MS.Services.Identity.Persistence.Repositories;

public class DocumentRepository : Repository<Document, IdentityDbContext>, IDocumentRepository
{
    public DocumentRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }
}