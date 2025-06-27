using Microsoft.Extensions.DependencyInjection;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Interfaces;
using MS.Services.Identity.Infrastructure.Clients.Keycloak.Services;

namespace MS.Services.Identity.Infrastructure.Clients.Keycloak;

public static class ServiceExtensions
{
    public static void AddKeycloakServices(this IServiceCollection serviceCollection)
    {        
        serviceCollection.AddScoped<IKeycloakTokenService, KeycloakBaseService>();
        serviceCollection.AddScoped<IKeycloakUserService, KeycloakUserService>();
        serviceCollection.AddScoped<IKeycloakRoleService, KeycloakRoleService>();
        serviceCollection.AddScoped<IKeycloakRoleMappingService, KeycloakRoleMappingService>();
        serviceCollection.AddScoped<IKeycloakClientService, KeycloakClientService>();
        serviceCollection.AddScoped<IKeycloakGroupService, KeycloakGroupService>();
        serviceCollection.AddScoped<IKeycloakAccountService, KeycloakAccountService>();
    }
}
