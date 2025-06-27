using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

public class ErrorModel
{
    public string error { get; set; } = null!;
    public string error_description { get; set; } = null!;
}
