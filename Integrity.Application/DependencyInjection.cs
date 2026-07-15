using Integrity.Application.Interfaces;
using Integrity.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Integrity.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddSingleton<IConnectionProfileService, ConnectionProfileService>();
        
        // Application services

        return services;
    }
}