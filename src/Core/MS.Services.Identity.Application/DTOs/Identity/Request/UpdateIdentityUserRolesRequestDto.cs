using Newtonsoft.Json;
using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Application.DTOs.Identity.Request;

public class UpdateIdentityUserRolesRequestDto
{
    public Guid IdentityRefId { get; set; }
    public List<string>? CurrentRoles { get; set; }
    public List<string>? DeletedRoles { get; set; }
    
}
