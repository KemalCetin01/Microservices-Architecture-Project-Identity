using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MS.Services.Core.Base.Extentions;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Extensions;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Filters.UserB2BFilters;

namespace MS.Services.Identity.Persistence.Extensions;

public static class QueryableSortExtensions
{
    public static IQueryable<UserB2B> SortByModel(this IQueryable<UserB2B> query, SortModel? sortModel)
    {
        return sortModel switch
        {
            _ when sortModel == null =>
                query.OrderByDescending(x => x.User.CreatedDate),
            _ when sortModel.Field.Equals("fullName", StringComparison.InvariantCultureIgnoreCase) =>
                query.OrderByDirection(x => x.User.FirstName, sortModel.Direction).OrderByDirection(x => x.User.LastName, sortModel.Direction),
            _ when sortModel.Field.Equals("email", StringComparison.InvariantCultureIgnoreCase) =>
                query.OrderByDirection(x => x.User.Email, sortModel.Direction),
            _ => query
        };
    }

    public static IQueryable<CurrentAccount> SortByModel(this IQueryable<CurrentAccount> query, SortModel? sortModel)
    {
        return sortModel switch
        {
            _ when sortModel == null =>
                query.OrderByDescending(x => x.CreatedDate),
            _ when sortModel.Field.Equals("erpRefId", StringComparison.InvariantCultureIgnoreCase) =>
                query.OrderByDirection(x => x.ErpRefId, sortModel.Direction),
            _ when sortModel.Field.Equals("code", StringComparison.InvariantCultureIgnoreCase) =>
                query.OrderByDirection(x => x.Code, sortModel.Direction),
            _ when sortModel.Field.Equals("name", StringComparison.InvariantCultureIgnoreCase) =>
                query.OrderByDirection(x => x.CurrentAccountName, sortModel.Direction),
            _ => query
        };
    }

}
