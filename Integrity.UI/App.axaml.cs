using System;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Integrity.Presentation.Services;
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
        var coordinator = _serviceProvider.GetRequiredService<ApplicationCoordinator>();

        await coordinator.StartAsync();
        
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = _serviceProvider.GetRequiredService<MainWindow>();

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
}