using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;

namespace Integrity.Application.Services;

public class ApplicationHost (IStartupService startupService, HttpConsumer httpConsumer) : IApplicationHost
{
    public ConnectionProfile? ActiveConnectionProfile {
        get;
        private set;
    }
    
    public async Task<StartupResult> StartAsync()
    {
        var context = new OperationContext
        {
            Operation = "API Liveliness Check"
        };
        
        try
        {
            var response = await httpConsumer.FromGetAsync(context);
            
            if(!response.IsSuccessStatusCode)
            {
                return new StartupResult(StartupStage.ApiError);
            }
        }
        catch (Exception ex)
        {
            return new StartupResult(StartupStage.ApiError);
        }
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