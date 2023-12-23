using HomeAssistant.BusinessLogic.Contracts.Ports;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAssistant.Services.DI;

public static class DependencyExtensions
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
    {
        services.AddSingleton<DataContext>();
        services.AddSingleton<IRelaysService, RelaysService>();
        services.AddSingleton<IMqttClient, MqttClient>();

        return services;
    }
}
