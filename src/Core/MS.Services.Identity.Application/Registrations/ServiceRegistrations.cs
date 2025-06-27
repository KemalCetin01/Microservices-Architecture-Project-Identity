using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MS.Services.Core.Caching.Redis;
using MS.Services.Core.Data.Data.Extensions;
//using MS.Services.Core.Messaging.Event;
//using MS.Services.Core.Messaging.Kafka;
using MS.Services.Identity.Application.Behaviours;
using MS.Services.Identity.Application.Handlers.External.LogoErp.Events.CurrentAccounts;
using MS.Services.Identity.Application.Helpers.Utility;
using MS.Services.Identity.Application.Messages;
using System.Reflection;

namespace MS.Services.Identity.Application.Registrations;

public static class ServiceRegistrations
{
    public static void AddApplicationLayer(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
      
        serviceCollection.AddAutoMapper(Assembly.GetExecutingAssembly());
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        ValidatorOptions.Global.PropertyNameResolver = CamelCasePropertyNameResolver.ResolvePropertyName;

        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorWithResponse<,>));
        serviceCollection.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorWithoutResponse<,>));


        serviceCollection.AddDataLayer(configuration);
        serviceCollection.AddRedisCache(configuration);
        //serviceCollection.AddEventLayer(configuration);

        //serviceCollection.AddKafkaConsumer<string, LogoErpCurrentAccountMessage, LogoErpCurrentAccountHandler>(configuration);

        //serviceCollection.AddKafkaProducer<string, LogoErpCurrentAccountMessage>(configuration);

        //serviceCollection.AddKafkaHealthCheck(configuration);
    }
}