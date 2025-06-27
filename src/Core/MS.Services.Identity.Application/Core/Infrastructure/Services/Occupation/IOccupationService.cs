using MS.Services.Identity.Application.Handlers.Occupations.Commands;
using MS.Services.Identity.Application.Handlers.Occupations.DTOs;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Models.FKeyModel;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface IOccupationService : IScopedService
{
    Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken);
    Task<OccupationDTO> GetByIdAsync(int id, CancellationToken cancellationToken  );
    Task<PagedResponse<OccupationDTO>> SearchAsync(SearchQueryModel<SearchOccupationsQueryFilterModel> searchQuery, CancellationToken cancellationToken  );

    Task<OccupationDTO> AddAsync(CreateOccupationCommand createOccupationCommand, CancellationToken cancellationToken  );
    Task<OccupationDTO> UpdateAsync(UpdateOccupationCommand updateOccupationCommand, CancellationToken cancellationToken  );

    Task DeleteAsync(int id, CancellationToken cancellationToken  );

}
