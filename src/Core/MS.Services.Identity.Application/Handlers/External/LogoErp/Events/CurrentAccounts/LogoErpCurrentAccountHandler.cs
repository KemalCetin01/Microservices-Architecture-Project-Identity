//using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using MS.Services.Core.ExceptionHandling.Exceptions;
//using MS.Services.Core.Messaging.Kafka.Consumer;
using MS.Services.Identity.Application.Core.Infrastructure.Services;
using MS.Services.Identity.Application.Messages;

namespace MS.Services.Identity.Application.Handlers.External.LogoErp.Events.CurrentAccounts;

public class LogoErpCurrentAccountHandler //: IKafkaHandler<LogoErpCurrentAccountMessage, Error>
{
    //private readonly IServiceProvider _serviceProvider;

    //public LogoErpCurrentAccountHandler(IServiceProvider serviceProvider)
    //{
    //    _serviceProvider = serviceProvider;
    //}

    //public async Task OnMessageDelivered(LogoErpCurrentAccountMessage message,
    //    CancellationToken cancellationToken = new())
    //{
    //    using var scope = _serviceProvider.CreateScope();
    //    var currentAccountService = scope.ServiceProvider.GetRequiredService<ICurrentAccountService>();
    //    await currentAccountService.CreateOrUpdateFromMessageAsync(message, cancellationToken);
    //}

    //public Task OnErrorOccured(Error error, CancellationToken cancellationToken = new())
    //{
    //    throw new ApiException(error.Reason, error.Code.ToString());
    //}
}
