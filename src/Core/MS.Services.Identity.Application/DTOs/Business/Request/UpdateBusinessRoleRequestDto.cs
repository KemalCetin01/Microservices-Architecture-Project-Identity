using MS.Services.Core.Base.Handlers;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.DTOs.AddressLocation.Request;
using MS.Services.Identity.Domain.Enums;

namespace MS.Services.Identity.Application.DTOs.Business.Request;

public class UpdateBusinessRoleRequestDto
{
    public Guid BusinessId { get; set; }
    public Guid RoleRefId { get; set; }
    public string Name { get; set; } = null!;
   
}