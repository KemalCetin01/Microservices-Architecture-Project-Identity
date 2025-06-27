using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Persistence.Repositories;

public class NumberOfEmployeeRepository : Repository<NumberOfEmployee, IdentityDbContext>, INumberOfEmployeeRepository
{
    public NumberOfEmployeeRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ICollection<LabelIntValueModel>> GetFKeyListAsync(CancellationToken cancellationToken)
    => await Queryable().Where(x => !x.IsDeleted && x.Status != Domain.Enums.StatusEnum.Inactive)
            .Select(x => new LabelIntValueModel
            {
                Value = x.Id,
                Label = x.Name,
            })
        .ToListAsync();
    public async Task<bool> HasNumberOfEmployeeExists(string name, int? id, CancellationToken cancellationToken)
    {

        return await Queryable().AnyAsync(x => x.Name == name && x.Id != id && !x.IsDeleted);
    }

    public async Task<SearchListModel<NumberOfEmployee>> SearchAsync(SearchQueryModel<SearchNumberOfEmployeeQueryFilterModel> searchQuery, CancellationToken cancellationToken)
    {
        var result = Queryable().Where(x => !x.IsDeleted).AsNoTracking();
        if (searchQuery.Filter != null)
        {
            if (searchQuery.Filter.name != null)
                result = result.Where(x => x.Name.ToUpper().Contains(searchQuery.Filter.name.ToUpper()));
            if (searchQuery.Filter.Status != 0 || (searchQuery.Filter.Status == 0 && searchQuery.Filter.name == null))
                result = result.Where(x => x.Status == searchQuery.Filter.Status);
        }
        return await SearchAsync(result, searchQuery, cancellationToken);
    }

}

