using MS.Services.Identity.Domain.Entities;

namespace MS.Services.Identity.Application.Core.Persistence.Repositories;

public interface IIdentityIdSequenceRepository
{
    Task<IdentityIdSequence?> GetAndUpdateByEntity(string entityName);
}
