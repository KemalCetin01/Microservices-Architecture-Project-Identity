using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.DTOs.Business.Request;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Application.DTOs.Business.Response;
using MS.Services.Core.Base.Dtos.Response;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services
{
    public interface IBusinessService : IScopedService
    {
        Task<GetBusinessResponseDto> GetByIdAsync(Guid Id, CancellationToken cancellationToken);
        Task<GetBusinessResponseDto> CreateAsync(CreateBusinessRequestDTO model, CancellationToken cancellationToken);
        Task<GetBusinessResponseDto> UpdateAsync(UpdateBusinessRequestDto model, CancellationToken cancellationToken);
        Task DeleteAsync(Guid Id, CancellationToken cancellationToken);
        Task<UpdateBusinessReviewStatusResponseDto> UpdateReviewStatusAsync(UpdateBusinessReviewStatusRequestDto model, CancellationToken cancellationToken);
        Task<PagedResponse<SearchBusinessResponseDto>> SearchAsync(SearchQueryModel<BusinessSearchFilter> searchQuery, CancellationToken cancellationToken);
        Task<ListResponse<LabelValueResponse>> GetFKeyBusinessesAsync(string search, CancellationToken cancellationToken = default);

    }
}
