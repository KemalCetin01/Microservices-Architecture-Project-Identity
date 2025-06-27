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
    public static IQueryable<Business> AddFilter(this IQueryable<Business> query, BusinessSearchFilter filter) 
    {
        if (filter == null) return query;

        if (!string.IsNullOrWhiteSpace(filter.Name)) 
            query = query.Where(x => x.Name.Contains(filter.Name));

        if (!string.IsNullOrWhiteSpace(filter.Code)) 
            query = query.Where(x => x.Code.Contains(filter.Code));

        if (!string.IsNullOrWhiteSpace(filter.CountryCode)) 
            query = query.Where(x => x.BillingAddress != null && x.BillingAddress.AddressLocation.CountryCode == filter.CountryCode);

        if (!filter.RepresentativeIds.IsNullOrEmpty())
            query = query.Where(x => x.RepresentativeId.HasValue && filter!.RepresentativeIds!.Contains(x.RepresentativeId.Value));

        if (filter.BusinessType != null) 
            query = query.Where(x => x.BillingAddress != null && x.BillingAddress.AddressLocation != null && x.BillingAddress.AddressLocation.CountryCode != null
                && ((filter.BusinessType==Domain.Enums.BusinessTypeEnum.Domestic && x.BillingAddress.AddressLocation.CountryCode == BusinessConstants.TurkeyCountryCode)
                    || (filter.BusinessType==Domain.Enums.BusinessTypeEnum.International && x.BillingAddress.AddressLocation.CountryCode != BusinessConstants.TurkeyCountryCode)));

        if (filter.BusinessStatus != null)
            query = query.Where(x => x.BusinessStatusId == (int)filter.BusinessStatus);

        if (filter.ReviewStatus != null) 
            query = query.Where(x => x.ReviewStatus == filter.ReviewStatus);

        return query;
    }

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
