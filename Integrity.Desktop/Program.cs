using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Integrity.Infrastructure;

namespace Integrity.Desktop;

sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        var serviceProvider = ApplicationHost.Run(args);

        BuildAvaloniaApp()
            .AfterSetup(builder =>
            {
                var app = (App)builder.Instance!;
                app._serviceProvider = serviceProvider;
            })
            .StartWithClassicDesktopLifetime(args);
    }
    
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
#if DEBUG
            .WithDeveloperTools()
#endif
            .WithInterFont()
            .LogToTrace();
}