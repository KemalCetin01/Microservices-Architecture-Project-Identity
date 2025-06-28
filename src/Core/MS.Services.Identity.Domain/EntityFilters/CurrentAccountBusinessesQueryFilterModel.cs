using MS.Services.Core.Data.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Domain.EntityFilters;

public class CurrentAccountBusinessesQueryFilterModel : IFilterModel
{
    public Guid BusinessId { get; set; }
}
