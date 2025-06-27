using Newtonsoft.Json;
using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Application.DTOs.Identity.Request;
public class IdentityLoginRequestDto
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}