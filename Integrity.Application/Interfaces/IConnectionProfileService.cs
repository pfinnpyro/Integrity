using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;

namespace Integrity.Application.Interfaces;

public interface IConnectionProfileService
{
    Task<ConnectionProfile?> GetActiveConnectionProfileAsync();
    Task<List<ConnectionProfile>> GetAllConnectionProfilesAsync();
    Task<OperationResult<Unit>> SaveConnectionProfileAsync(ConnectionProfile profile);
    Task DeleteConnectionProfileAsync(Guid id);
    Task SetActiveConnectionProfileAsync(Guid id);
    OperationResult<Unit> ValidateConnectionProfile(ConnectionProfile profile);
    Task<OperationResult<Unit>> TestConnectionAsync(ConnectionProfile profile);

}