using HomeAssistant.BusinessLogic.Contracts.Ports;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace HomeAssistant.Services.DI;

public static class DependencyExtensions
{
    public static IServiceCollection AddServicesLayer(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlite("Data Source=HomeAssistant.db");
        });

        services.AddScoped<IRelaysService, RelaysService>();

        return services;
    }
}
