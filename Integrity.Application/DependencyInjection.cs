using Integrity.Application.Interfaces;
using Integrity.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Integrity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddTransient<IStartupService, StartupService>();
        services.AddSingleton<IConnectionProfileService, ConnectionProfileService>();
        services.AddSingleton<IApplicationHost, ApplicationHost>();
        services.AddTransient<IDatabaseConnectionProviderResolver,
            DatabaseConnectionProviderResolver>();
        // Application services

        return services;
    }
}