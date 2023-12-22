using HomeAssistant.BusinessLogic;
using HomeAssistant.BusinessLogic.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAssistant.Services.DI;

public static class DependencyExtensions
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddScoped<IMqttMessageHandler, MqttMessageHandler>();
        services.AddScoped<IRelaysBusinessLogic, RelaysBusinessLogic>();

        return services;
    }
}
