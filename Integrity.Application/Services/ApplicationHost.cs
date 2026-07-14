using Integrity.Application.Models.Results;

namespace Integrity.Application.Services;

public class ApplicationHost (IStartupService startupService) : IApplicationHost
{
    public async Task StartAsync()
    {
        var result = await startupService.InitializeAsync();
        
        switch (result.Stage)
        {
            case StartupStage.AuthenticationRequired:
                break;
            case StartupStage.ConfigurationRequired:
                break;
        }
    }
}