using MS.Services.Core.Networking.Http.Services;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;

public interface IKeycloakTokenService : IRestService
{
    Task GenerateTokenAsync(CancellationToken cancellationToken  );
}

