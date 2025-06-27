using MS.Services.Core.Base.Dtos;
using MS.Services.Identity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Services.Identity.Application.Handlers.NumberOfEmployees.DTOs;

public class NumberOfEmployeeDTO : IResponse
{
    public int Id { get; init; }
    public string Name { get; set; } = null!;
    public StatusEnum Status { get; set; }
    public DateTime CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }
}
