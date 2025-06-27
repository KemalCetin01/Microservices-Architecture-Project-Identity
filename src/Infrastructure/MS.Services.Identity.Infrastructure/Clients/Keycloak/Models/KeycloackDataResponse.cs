using MS.Services.Core.Networking.Http.Models;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Models;

public class KeycloackDataResponse<T> : IRestResponse
{
    public T? Data { get; set; }

}
