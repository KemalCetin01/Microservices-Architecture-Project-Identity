using MS.Services.Identity.Application.Handlers.Positions.Commands;
using MS.Services.Identity.Application.Handlers.Positions.DTOs;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Models.FKeyModel;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface IPositionService : IScopedService
{
    Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken  );
    Task<PositionDTO> GetByIdAsync(int id, CancellationToken cancellationToken  );
    Task<PagedResponse<PositionDTO>> SearchAsync(SearchQueryModel<SearchPositionsQueryFilterModel> searchQuery, CancellationToken cancellationToken  );

    Task<PositionDTO> AddAsync(CreatePositionCommand createPositionCommand, CancellationToken cancellationToken  );
    Task<PositionDTO> UpdateAsync(UpdatePositionCommand updatePositionCommand, CancellationToken cancellationToken  );

    Task DeleteAsync(int id, CancellationToken cancellationToken  );

}
