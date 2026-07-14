using Integrity.Application.Interfaces;
using Integrity.Application.Models;

namespace Integrity.Application.Services;

public class StartupService(IConnectionProfileStore connectionProfileStore) : IStartupService
{

    public async Task<StartupResult> InitializeAsync()
    {
        var profile = await connectionProfileStore
            .GetActiveConnectionProfileAsync();

        if(profile is null)
        {
            return new StartupResult(StartupStage.ConfigurationRequired);
        }

        return new StartupResult(StartupStage.AuthenticationRequired);
    }
}