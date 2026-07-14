using Integrity.Application.Models;
using Integrity.Application.Services;
using Integrity.Presentation.Navigation;
using Integrity.Presentation.ViewModels;

namespace Integrity.Presentation.Services;

public class ApplicationCoordinator(IApplicationHost applicationHost, INavigationService navigationService)
{
    public async Task StartAsync()
    {
        var result = await applicationHost.StartAsync();

        switch (result.Stage)
        {
            case StartupStage.AuthenticationRequired:
                await navigationService.NavigateToAsync<LoginViewModel>();
                break;
            case StartupStage.ConfigurationRequired:
                await navigationService.NavigateToAsync<DatabaseConfigurationViewModel>();
                break;
        }
    }
}