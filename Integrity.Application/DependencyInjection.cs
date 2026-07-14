using Integrity.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Integrity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddTransient<IStartupService, StartupService>();
        
        services.AddTransient<IApplicationHost, ApplicationHost>();
        
        // Application services

        return services;
    }
}