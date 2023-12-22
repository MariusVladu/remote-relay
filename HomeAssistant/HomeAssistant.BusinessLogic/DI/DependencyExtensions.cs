using HomeAssistant.BusinessLogic;
using HomeAssistant.BusinessLogic.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAssistant.Services.DI;

public static class DependencyExtensions
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddSingleton<IMqttMessageHandler, MqttMessageHandler>();
        services.AddSingleton<IRelaysBusinessLogic, RelaysBusinessLogic>();

        return services;
    }
}
