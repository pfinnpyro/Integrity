using Integrity.Application.Interfaces;
using Integrity.Application.Models;

namespace Integrity.Application.Services;

public class StartupService(IConnectionProfileService connectionProfileService, 
    IDatabaseProviderResolver providerResolver) : IStartupService
{

    public async Task<StartupResult> InitializeAsync()
    {
        return new StartupResult(StartupStage.ConfigurationRequired);
        
        /*// var profile = await connectionProfileService
        //     .GetActiveConnectionProfileAsync();
        //
        // if(profile is null)
        // {
        //     return new StartupResult(StartupStage.ConfigurationRequired);
        // }
        //
        // return new StartupResult(StartupStage.AuthenticationRequired);*/
    }
}