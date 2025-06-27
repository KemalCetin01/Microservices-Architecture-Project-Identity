using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.AddressLocation.Request;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.DTOs.Business.Request;

public class CreateBusinessRoleRequestDto
{
    public Guid BusinessId { get; set; }
    public string Name { get; set; } = null!;
   
}