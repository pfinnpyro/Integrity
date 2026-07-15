using Integrity.Application.Interfaces;
using Integrity.Infrastructure.Configuration;
using Integrity.Infrastructure.Database;
using Microsoft.Extensions.DependencyInjection;

namespace Integrity.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IConnectionProfileStore, JsonConnectionProfileStore>();
        services.AddTransient<IDatabaseConnectionProvider, SqlConnectionProvider>();

        return services;
    }
}