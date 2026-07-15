using Integrity.Application.Interfaces;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;

namespace Integrity.Application.Services;

public class ConnectionProfileService (
    IConnectionProfileStore _connectionProfileStore) : IConnectionProfileService
{
    
    public Task<List<ConnectionProfile>> GetAllConnectionProfilesAsync()
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

    public async Task<OperationResult<Unit>> SaveConnectionProfileAsync(ConnectionProfile profile)
    {
        var context = new OperationContext
        {
            EntityId = profile.Id,
            EntityType = nameof(ConnectionProfile)
        };
        
        var result = ValidateConnectionProfile(profile);
        
        if( !result.IsSuccess )
            return OperationResult<Unit>.Failure(context, result.Errors.ToArray());
        
        var id = await _connectionProfileStore.SaveConnectionProfileAsync(profile);

        return OperationResult.Success();
    }

    public OperationResult<Unit> ValidateConnectionProfile(ConnectionProfile profile)
    {
        var context = new OperationContext
        {
            EntityId = profile.Id,
            EntityType = nameof(ConnectionProfile)
        };
        
        var result = new OperationResult<Unit>();
        
        if(string.IsNullOrEmpty(profile.Name))
            result.AddError(Errors.InvalidName);
        if (string.IsNullOrEmpty(profile.Server))
            result.AddError(Errors.InvalidServer);
        if (string.IsNullOrEmpty(profile.Database))
            result.AddError(Errors.InvalidDatabase);
        
        if(result.Errors.Count > 0)
            return OperationResult<Unit>.Failure(context, result.Errors.ToArray());

        return OperationResult.Success();
    }
    
    public async Task<OperationResult<Unit>> TestConnectionAsync(ConnectionProfile profile)
    {
        throw new NotImplementedException();
    }
}