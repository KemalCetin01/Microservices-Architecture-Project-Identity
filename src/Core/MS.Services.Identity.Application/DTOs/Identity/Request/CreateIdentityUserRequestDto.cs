using Newtonsoft.Json;
using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Application.DTOs.Identity.Request;

public class CreateIdentityUserRequestDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}