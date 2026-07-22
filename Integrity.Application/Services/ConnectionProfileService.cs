using System.Security;
using Integrity.Application.Interfaces;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;

namespace Integrity.Application.Services;

public class ConnectionProfileService (
    IConnectionProfileStore _connectionProfileStore,
    IDatabaseProviderResolver providerResolver) : IConnectionProfileService
{
    
    public Task<OperationResult<List<ConnectionProfile>>> GetAllConnectionProfilesAsync()
    {
        return _connectionProfileStore.GetAllConnectionProfilesAsync();
    }
    
    public Task<ConnectionProfile?> GetActiveConnectionProfileAsync()
    {
        return _connectionProfileStore.GetActiveConnectionProfileAsync();
    }
    
    public Task DeleteConnectionProfileAsync(Guid id)
    {
        return _connectionProfileStore.DeleteConnectionProfileAsync(id);
    }

    public Task SetActiveConnectionProfileAsync(Guid id)
    {
        return _connectionProfileStore.SetActiveConnectionProfileAsync(id);
    }

    public async Task<OperationResult> SaveConnectionProfileAsync(ConnectionProfile profile)
    {
        var context = new OperationContext
        {
            EntityId = profile.Id,
            EntityType = nameof(ConnectionProfile)
        };
        
        var result = ValidateConnectionProfile(profile);
        
        if( !result.IsSuccess )
            return OperationResult.Failure(context, result.Errors.ToArray());
        
        var id = await _connectionProfileStore.SaveConnectionProfileAsync(profile);

        return OperationResult.Success(context);
    }

    public OperationResult ValidateConnectionProfile(ConnectionProfile profile)
    {
        var context = new OperationContext
        {
            EntityId = profile.Id,
            EntityType = nameof(ConnectionProfile)
        };
        
        var errors = new List<Error>();
        
        if(string.IsNullOrWhiteSpace(profile.Name))
            errors.Add(Errors.InvalidName);
        if (string.IsNullOrWhiteSpace(profile.Server))
            errors.Add(Errors.InvalidServer);
        if (string.IsNullOrWhiteSpace(profile.Database))
            errors.Add(Errors.InvalidDatabase);
        
        if(errors.Count > 0)
            return OperationResult.Failure(context, errors.ToArray());

        var providerResult =
            providerResolver.Resolve(profile.Provider);
        
        if(!providerResult.IsSuccess)
            return providerResult.ToFailure();
        
        return providerResult.Value.ValidateConnectionProfile(profile);
    }
    
    public async Task<OperationResult> TestConnectionAsync(ConnectionProfile profile, string password)
    {
        var provider = providerResolver.Resolve(profile.Provider);
        return await provider.Value.CheckHealthAsync(profile, password);
    }
}