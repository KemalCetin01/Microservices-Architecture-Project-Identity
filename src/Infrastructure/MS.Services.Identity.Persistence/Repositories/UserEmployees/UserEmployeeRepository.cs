using System.Linq.Expressions;
using MS.Services.Core.Base.Extentions;
using MS.Services.Core.Data.Data.Concrete;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Core.Persistence.Repositories;
using MS.Services.Identity.Application.Handlers.UserEmployees.DTOs;
using MS.Services.Identity.Application.Models.FKeyModel;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Filters.CurrentAccountFilters;
using MS.Services.Identity.Persistence.Context;
using static MS.Services.Identity.Application.Constants.Constants;

namespace MS.Services.Identity.Persistence.Repositories;

public class UserEmployeeRepository : Repository<UserEmployee, IdentityDbContext>, IUserEmployeeRepository
{
    public UserEmployeeRepository(IdentityDbContext dbContext) : base(dbContext)
    {
    }



    public async Task<UserEmployee> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
    {
        return await Queryable().Include(x => x.User).Include(x => x.EmployeeRole).Include(x => x.EmployeeRole).Where(x => x.UserId == Id && x.User.IsDeleted == false).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<LabelValueModel>> GetEmployees(string search, CancellationToken cancellationToken  )
    {

        var q = Queryable().Include(x => x.User).Where(x => !x.User.IsDeleted);
        var searchQueryModel = new SearchQueryModel<CurrentAccountValueLabelQueryServiceFilter>();
        if (!string.IsNullOrWhiteSpace(search)) { searchQueryModel.GlobalSearch = search; }
        var result = await SearchAsync(q, searchQueryModel);
        return result.Data.Select(x => new LabelValueModel()
        {
            Label = x.User.FirstName + " " + x.User.LastName + " (" + x.User.Email + ")",
            Value = x.UserId
        }).ToList();
    }
    public async Task<int> GetActiveUserInRoleCountAsync(Guid roleId, CancellationToken cancellationToken)
    =>await Queryable().Where(x=>!x.IsDeleted && x.EmployeeRoleId==roleId).CountAsync(cancellationToken);
    public async Task<SearchListModel<UserEmployee>> SearchAsync(
      SearchQueryModel<SearchUserEmployeesQueryFilterModel> searchQuery, CancellationToken cancellationToken)
    {
        var result = Queryable()
            .Include(x => x.User)
            .Include(x => x.EmployeeRole)
             .AsNoTrackingWithIdentityResolution();
        if (searchQuery.Filter != null)
        {
            if (searchQuery.Filter.FullName != null)
                result = result.Where(x => (x.User.FirstName + " " + x.User.LastName).ToUpper().Contains(searchQuery.Filter.FullName.ToUpper()));

            if (searchQuery.Filter.Email != null)
                result = result.Where(x => x.User.Email.ToUpper().Contains(searchQuery.Filter.Email.ToUpper()));

            if (searchQuery.Filter.PhoneNumber != null)
                result = result.Where(x => x.PhoneNumber.Contains(searchQuery.Filter.PhoneNumber));

            if (searchQuery.Filter.Role != null)
                result = result.Where(x => x.EmployeeRole.Name.Contains(searchQuery.Filter.Role));

            if (searchQuery.Filter.PhoneNumber != null)
                result = result.Where(x => x.PhoneNumber.Contains(searchQuery.Filter.PhoneNumber));

            if (searchQuery.Filter.FirstDiscountRate != null)
            {
                result = result.Where(x => x.EmployeeRole.DiscountRate >= searchQuery.Filter.FirstDiscountRate);
            }
            if (searchQuery.Filter.LastDiscountRate != null && searchQuery.Filter.LastDiscountRate > 0)
            {
                result = result.Where(x => x.EmployeeRole.DiscountRate <= searchQuery.Filter.LastDiscountRate);
            }

            if (searchQuery.Filter.lastRangeLastEntryDate != null)
            {
                if (searchQuery.Filter.FirstDiscountRate == null)
                    searchQuery.Filter.FirstDiscountRate = 0;
                result = result.Where(x => x.LastDateEntry > searchQuery.Filter.firstRangeLastEntryDate && x.LastDateEntry < searchQuery.Filter.lastRangeLastEntryDate);
            }
        }

        return await SearchAsync(result, searchQuery, cancellationToken);
    }

    protected override IQueryable<UserEmployee> GlobalSearchQuery(IQueryable<UserEmployee> query, string? searchQuery)
    {
        if (!string.IsNullOrWhiteSpace(searchQuery))
        {
            var globalSearch = searchQuery.ToUpper();
            query = query.Where(x => x.PhoneNumber!.Contains(globalSearch)
                             || x.EmployeeRole!.Description.ToUpper().Contains(globalSearch)
                             || (x.User.FirstName + " " + x.User.LastName).ToUpper().Contains(globalSearch)
                             || x.User.Email!.ToUpper().Contains(globalSearch));
        }

        return query;
    }
    protected override IQueryable<UserEmployee> SearchOrderQuery(IQueryable<UserEmployee> query, SortModel? sortModel)
    {
        if (sortModel == null)
        {
            return query;
        }
        switch (sortModel.Field)
        {
            case EmployeeConstants.FullName:
                query = query.OrderByDirection(x => (x.User.FirstName + " " + x.User.LastName), sortModel.Direction);
                break;
            case EmployeeConstants.PhoneNumber:
                query = query.OrderByDirection(x => x.PhoneNumber, sortModel.Direction);
                break;
            case EmployeeConstants.Role:
                query = query.OrderByDirection(x => x.EmployeeRole.Name, sortModel.Direction);
                break;
            case EmployeeConstants.DiscountRate:
                query = query.OrderByDirection(x => x.EmployeeRole.DiscountRate, sortModel.Direction);
                break;
            case EmployeeConstants.Email:
                query = query.OrderByDirection(x => x.User.Email, sortModel.Direction);
                break;
            case EmployeeConstants.LastDateEntry:
                query = query.OrderByDirection(x => x.LastDateEntry, sortModel.Direction);
                break;
        }
        
        return query;
    }
}
