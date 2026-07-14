using Integrity.Application.Models;

namespace Integrity.Application.Services;

public class ApplicationHost (IStartupService startupService) : IApplicationHost
{
    public async Task<StartupResult> StartAsync()
    {
        return await startupService.InitializeAsync();
    }
}