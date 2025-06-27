using Newtonsoft.Json;
using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Application.DTOs.Identity.Request;
public class CreateIdentityRoleRequestDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}