using System;
using Integrity.Application;
using Integrity.Infrastructure;
using Integrity.Presentation;
using Integrity.UI;
using Microsoft.Extensions.DependencyInjection;

namespace Integrity.Desktop;

public class ApplicationHost
{
    public static IServiceProvider Run(string[] args)
    {
        var services = new ServiceCollection();
        ConfigureServices(services);
        return services.BuildServiceProvider();
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddInfrastructure();
        services.AddPresentation();
        services.AddUi();
    }
}