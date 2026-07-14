using Integrity.Presentation.Navigation;
using Integrity.Presentation.Services;
using Integrity.Presentation.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Integrity.Presentation;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(
        this IServiceCollection services)
    {
        services.AddSingleton<ShellViewModel>();
        services.AddTransient<ApplicationHost>();
        services.AddTransient<INavigationService, NavigationService>();
    
        return services;
    }
}