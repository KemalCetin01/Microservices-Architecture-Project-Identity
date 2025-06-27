using Microsoft.Extensions.DependencyInjection;
using MS.Services.Identity.Application.Core.Infrastructure.External;
using MS.Services.Identity.Application.Core.Infrastructure.External.Identity;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Infrastructure.Clients.Keycloak;
using MS.Services.Identity.Infrastructure.External;
using MS.Services.Identity.Infrastructure.External.Identity;
using MS.Services.Identity.Infrastructure.Services;

namespace MS.Services.Identity.Infrastructure;

public static class ServiceRegistration
{
    public static void AddInfrastructureLayer(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddKeycloakServices();
        serviceCollection.AddScoped<IIdentityB2CService, IdentityKeycloakB2CService>();
        serviceCollection.AddScoped<IIdentityB2BService, IdentityKeycloakB2BService>();
        serviceCollection.AddScoped<IIdentityEmployeeService, IdentityKeycloakEmployeeService>();
        serviceCollection.AddScoped<IBusinessBillingAddressService, BusinessBillingAddressService>();
    }
}
