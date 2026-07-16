using System.Security;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;

namespace Integrity.Application.Interfaces;

public interface IDatabaseConnectionProvider
{
    DatabaseProvider Provider { get; }
    string GetConnectionString(ConnectionProfile profile, SecureString password);
    Task<OperationResult<Unit>> TestConnectionAsync(ConnectionProfile profile, SecureString password);
    
    OperationResult<Unit> ValidateConnectionProfile(ConnectionProfile profile);
}