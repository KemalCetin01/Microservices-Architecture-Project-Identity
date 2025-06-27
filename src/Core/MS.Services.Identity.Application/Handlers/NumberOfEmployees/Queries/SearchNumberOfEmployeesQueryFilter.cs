using MS.Services.Core.Base.Handlers.Search;
using MS.Services.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Application.Handlers.NumberOfEmployees.Queries;

public class SearchNumberOfEmployeesQueryFilter : IFilter
{
    public string name { get; set; }
    public StatusEnum Status { get; set; }

}
