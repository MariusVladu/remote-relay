using HomeAssistant.Services.DI;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAssistant.DI;

public static class DependencyResolver
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services
            .AddServicesLayer()
            .AddBusinessLogicLayer();

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
