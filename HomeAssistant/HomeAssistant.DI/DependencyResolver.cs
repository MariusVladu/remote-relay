using HomeAssistant.BusinessLogic;
using HomeAssistant.BusinessLogic.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAssistant.DI;

public static class DependencyResolver
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services.AddSingleton<IMqttMessageHandler, MqttMessageHandler>();

        return services;
    }

    public static IServiceProvider GetServiceProvider()
    {
        var services = new ServiceCollection();

        services.AddDependencies();

        return services.BuildServiceProvider(new ServiceProviderOptions
        {
            ValidateOnBuild = true,
            ValidateScopes = true,
        });
    }
}
