

using MS.Services.Core.Data.Data.Interface;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Persistence.Context;
using MS.Services.Identity.Persistence.Helpers;

namespace MS.Services.Identity.Persistence.Repositories;

public class IdentityIdSequenceRepository : IIdentityIdSequenceRepository
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IdentityDbContextOption _identityDbContextOption;

    public IdentityIdSequenceRepository(IdentityDbContextOption IdentityDbContextOption,
        ICurrentUserService currentUserService)
    {
        _identityDbContextOption = IdentityDbContextOption;
        _currentUserService = currentUserService;
    }

    public async Task<IdentityIdSequence?> GetAndUpdateByEntity(string entityName)
    {
        IdentityIdSequence? identityKey;

        await using (var identityDbContext =
                     new IdentityDbContext(_identityDbContextOption.GetIdentityDbContextOption(), _currentUserService))
        {
            identityKey =
                await identityDbContext.IdentityIdSequences.FirstOrDefaultAsync(x => x.Entity == entityName);
            if (identityKey == null) return null;

            bool isUpdateFailed;
            do
            {
                isUpdateFailed = false;
                try
                {
                    identityKey.Counter++;
                    await identityDbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    isUpdateFailed = true;
                    await ex.Entries.Single().ReloadAsync();
                }
            } while (isUpdateFailed);
        }

        return identityKey;
    }
}
