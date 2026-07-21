using System;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Integrity.Presentation.Services;
using Integrity.UI;
using Integrity.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Integrity;

public partial class App : Avalonia.Application
{

    public IServiceProvider _serviceProvider { get; set; } = null!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            var splash = new SplashWindow();
            desktop.MainWindow = splash;

            splash.Show();
            _ = StartApplicationAsync(desktop, splash);
        }
        else if (ApplicationLifetime is IActivityApplicationLifetime singleViewFactoryApplicationLifetime)
        {
            singleViewFactoryApplicationLifetime.MainViewFactory =
                () => _serviceProvider.GetRequiredService<MainView>();
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = _serviceProvider.GetRequiredService<MainView>();
        }
        
        base.OnFrameworkInitializationCompleted();
        
    }

    private async Task StartApplicationAsync(IClassicDesktopStyleApplicationLifetime desktop,
        SplashWindow splash)
    {
        var coordinator = _serviceProvider.GetRequiredService<ApplicationCoordinator>();
        await coordinator.StartAsync();
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        desktop.MainWindow = mainWindow;
        mainWindow.Show();
        splash.Close();
        
    }
}