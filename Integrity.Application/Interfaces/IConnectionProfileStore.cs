using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;

namespace Integrity.Application.Interfaces;

public interface IConnectionProfileStore
{ 
    Task<OperationResult<List<ConnectionProfile>>> GetAllConnectionProfilesAsync();

    Task<ConnectionProfile?> GetConnectionProfileAsync(Guid profileId);
    
    Task<ConnectionProfile?> GetActiveConnectionProfileAsync();
    
    Task<Guid> SaveConnectionProfileAsync(ConnectionProfile profile);
    
    Task<OperationResult<Unit>> SetActiveConnectionProfileAsync(Guid profileId);
    Task<OperationResult<Unit>> HasConnectionProfilesAsync();
    Task<OperationResult<Unit>> DeleteConnectionProfileAsync(Guid profileId);
}