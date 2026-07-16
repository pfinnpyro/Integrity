using System.Security;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;

namespace Integrity.Application.Interfaces;

public interface IConnectionProfileService
{
    Task<ConnectionProfile?> GetActiveConnectionProfileAsync();
    Task<OperationResult<List<ConnectionProfile>>> GetAllConnectionProfilesAsync();
    Task<OperationResult<Unit>> SaveConnectionProfileAsync(ConnectionProfile profile);
    Task DeleteConnectionProfileAsync(Guid id);
    Task SetActiveConnectionProfileAsync(Guid id);
    OperationResult<Unit> ValidateConnectionProfile(ConnectionProfile profile);
    Task<OperationResult<Unit>> TestConnectionAsync(ConnectionProfile profile, SecureString password);

}