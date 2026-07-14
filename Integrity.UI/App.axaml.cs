using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using Integrity.Application.Services;
using Integrity.Presentation.ViewModels;
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
            desktop.MainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            
            var host = _serviceProvider.GetRequiredService<IApplicationHost>();

            await host.StartAsync();
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