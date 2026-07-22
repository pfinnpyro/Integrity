using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;

namespace Integrity.Application.Services;

public interface IApplicationHost
{
    Task<StartupResult> StartAsync();
    
    ConnectionProfile? ActiveConnectionProfile { get; }
    
    Task ConnectAsync(ConnectionProfile profile);
    
    OperationResult ActivateConnectionProfile(ConnectionProfile profile);
    

    
}