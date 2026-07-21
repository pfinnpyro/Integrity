using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;

namespace Integrity.Application.Services;

public class ApplicationHost (IStartupService startupService) : IApplicationHost
{
    public ConnectionProfile? ActiveConnectionProfile {
        get;
        private set;
    }
    
    public async Task<StartupResult> StartAsync()
    {
        var result = await startupService.InitializeAsync();
        
        if (result.Stage == StartupStage.DatabaseAuthenticationRequired)
        {
            ActiveConnectionProfile = result.Profile;
        }

        return result;
    }

    public OperationResult ActivateConnectionProfile(ConnectionProfile profile)
    {
        var context = new OperationContext
        {
            EntityId = profile.Id,
            EntityType = nameof(ConnectionProfile),
            Operation = "Set application connection profile"
        };
        
        ActiveConnectionProfile = profile;
        
        return OperationResult.Success(context);
    }
    
    public async Task ConnectAsync(ConnectionProfile profile)
    {
        ActivateConnectionProfile(profile);
    }
    
}