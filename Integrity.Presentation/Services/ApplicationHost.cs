using Integrity.Application.Models;
using Integrity.Application.Models.Results;
using Integrity.Application.Services;

namespace Integrity.Presentation.Services;

public class ApplicationHost(IStartupService startupService)
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