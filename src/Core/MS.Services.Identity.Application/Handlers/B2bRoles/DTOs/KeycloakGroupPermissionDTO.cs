using MS.Services.Core.Base.Dtos;

namespace MS.Services.Identity.Application.Handlers.B2bRoles.DTOs;

public class KeycloakGroupPermissionDTO : IResponse
{
    public string id { get; set; }
    public string name { get; set; }
    public string clientId { get; set; }
}
