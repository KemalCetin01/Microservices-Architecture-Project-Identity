using MS.Services.Identity.Application.Handlers.ActivityAreas.Commands;
using MS.Services.Identity.Application.Handlers.ActivityAreas.DTOs;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Models.FKeyModel;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface IActivityAreaService : IScopedService
{
    Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken  );
    Task<ActivityAreaDTO> GetByIdAsync(int id, CancellationToken cancellationToken  );
    Task<PagedResponse<ActivityAreaDTO>> SearchAsync(SearchQueryModel<SearchActivityAreasQueryFilterModel> searchQuery, CancellationToken cancellationToken  );

    Task<ActivityAreaDTO> AddAsync(CreateActivityAreaCommand createActivityAreaCommand, CancellationToken cancellationToken  );
    Task<ActivityAreaDTO> UpdateAsync(UpdateActivityAreaCommand updateActivityAreaCommand, CancellationToken cancellationToken  );

    Task DeleteAsync(int id, CancellationToken cancellationToken  );

}
