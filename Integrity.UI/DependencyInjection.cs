using Integrity.Views;
using Microsoft.Extensions.DependencyInjection;
using MainWindow = Integrity.UI.Windows.MainWindow;

namespace Integrity.UI;

public static class DependencyInjection
{
    public static IServiceCollection AddUi(
        this IServiceCollection services)
    {
        services.AddTransient<MainWindow>();
        services.AddTransient<MainView>();

        return services;
    }
}