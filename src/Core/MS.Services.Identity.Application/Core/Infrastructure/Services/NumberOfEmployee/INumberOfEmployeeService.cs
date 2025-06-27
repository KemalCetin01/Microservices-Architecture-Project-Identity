using MS.Services.Core.Base.IoC;
using MS.Services.Core.Base.Wrapper;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.Commands;
using MS.Services.Identity.Application.Handlers.NumberOfEmployees.DTOs;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.EntityFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Application.Core.Infrastructure.Services;

public interface INumberOfEmployeeService : IScopedService
{
    Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken);
    Task<NumberOfEmployeeDTO> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<PagedResponse<NumberOfEmployeeDTO>> SearchAsync(SearchQueryModel<SearchNumberOfEmployeeQueryFilterModel> searchQuery, CancellationToken cancellationToken);

    Task<NumberOfEmployeeDTO> AddAsync(CreateNumberOfEmployeeCommand createNumberOfEmployeeCommand, CancellationToken cancellationToken);
    Task<NumberOfEmployeeDTO> UpdateAsync(UpdateNumberOfEmployeeCommand updateNumberOfEmployeeCommand, CancellationToken cancellationToken);

    Task DeleteAsync(int id, CancellationToken cancellationToken);

}
