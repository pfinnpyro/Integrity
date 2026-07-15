using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;

namespace Integrity.Application.Interfaces;

public interface IDatabaseConnectionProvider
{
    DatabaseProvider Provider { get; }
    string GetConnectionString(ConnectionProfile profile, string password);
    Task<OperationResult<Unit>> TestConnectionAsync(ConnectionProfile profile, string password);
    
    OperationResult<Unit> ValidateConnectionProfile(ConnectionProfile profile);
}