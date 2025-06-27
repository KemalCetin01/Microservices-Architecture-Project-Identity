using MS.Services.Identity.Application.Handlers.Sectors.Commands;
using MS.Services.Identity.Application.Handlers.Sectors.DTOs;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Models.FKeyModel;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface ISectorService : IScopedService
{
    Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken);
    Task<SectorDTO> GetByIdAsync(int id, CancellationToken cancellationToken  );
    Task<PagedResponse<SectorDTO>> SearchAsync(SearchQueryModel<SearchSectorsQueryFilterModel> searchQuery, CancellationToken cancellationToken  );

    Task<SectorDTO> AddAsync(CreateSectorCommand createSectorCommand, CancellationToken cancellationToken  );
    Task<SectorDTO> UpdateAsync(UpdateSectorCommand updateSectorCommand, CancellationToken cancellationToken  );

    Task DeleteAsync(int id, CancellationToken cancellationToken  );

}
