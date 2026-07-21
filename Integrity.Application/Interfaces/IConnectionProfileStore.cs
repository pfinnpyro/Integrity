using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;

namespace Integrity.Application.Interfaces;

public interface IConnectionProfileStore
{ 
    Task<OperationResult<List<ConnectionProfile>>> GetAllConnectionProfilesAsync();

    Task<ConnectionProfile?> GetConnectionProfileAsync(Guid profileId);
    
    Task<ConnectionProfile?> GetActiveConnectionProfileAsync();
    
    Task<OperationResult<Guid>> SaveConnectionProfileAsync(ConnectionProfile profile);
    
    Task<OperationResult> SetActiveConnectionProfileAsync(Guid profileId);
    Task<OperationResult> HasConnectionProfilesAsync();
    Task<OperationResult> DeleteConnectionProfileAsync(Guid profileId);
}