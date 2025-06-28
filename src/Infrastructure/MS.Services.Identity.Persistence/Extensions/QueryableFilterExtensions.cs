using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Application.Extensions;
using MS.Services.Identity.Domain.EntityConstants;
using MS.Services.Identity.Domain.EntityFilters;
using MS.Services.Identity.Domain.Filters.UserB2BFilters;

namespace MS.Services.Identity.Persistence.Extensions;

public static class QueryableFilterExtensions
{
   

    public static IQueryable<UserB2B> AddFilter(this IQueryable<UserB2B> query, BusinessUserSearchFilter filter) 
    {
        if (!string.IsNullOrWhiteSpace(filter.FullName)) 
            query = query.Where(x => String.Concat(x.User.FirstName, " ", x.User.LastName).ToUpper().Contains(filter.FullName.ToUpper()));

        if (!string.IsNullOrWhiteSpace(filter.Email))
            query = query.Where(x => x.User.Email.ToUpper().Contains(filter.Email.ToUpper()));

        return query;
    }

    public static IQueryable<CurrentAccount> AddFilter(this IQueryable<CurrentAccount> query, BusinessCurrentAccountSearchFilter filter) 
    {
        if (!string.IsNullOrWhiteSpace(filter.CurrentAccountName)) 
            query = query.Where(x =>  x.CurrentAccountName != null && x.CurrentAccountName.ToUpper().Contains(filter.CurrentAccountName.ToUpper()));

        if (!string.IsNullOrWhiteSpace(filter.Code))
            query = query.Where(x => x.Code.ToUpper().Contains(filter.Code.ToUpper()));

        if (!string.IsNullOrWhiteSpace(filter.ErpRefId))
            query = query.Where(x => x.ErpRefId.ToUpper().Contains(filter.ErpRefId.ToUpper()));

        return query;
    }
}
