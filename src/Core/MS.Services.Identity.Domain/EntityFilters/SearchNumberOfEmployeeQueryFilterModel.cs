using MS.Services.Core.Data.Data.Models;
using MS.Services.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Domain.EntityFilters;

public class SearchNumberOfEmployeeQueryFilterModel : IFilterModel
{
    public string name { get; set; }
    public StatusEnum Status { get; set; }
}

