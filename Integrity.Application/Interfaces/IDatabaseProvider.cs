using System.Data.Common;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;

namespace Integrity.Application.Interfaces;

public interface IDatabaseProvider
{
    DatabaseProvider Provider { get; }
    Task<DbConnection> OpenConnectionAsync();
    Task<DatabaseMetadata> GetMetadataAsync();
    OperationResult ValidateConnectionProfile(ConnectionProfile profile);
    
    string GetConnectionString(ConnectionProfile profile, string password);
    Task<OperationResult<DatabaseMetadata>> CheckHealthAsync(ConnectionProfile profile, string password);
    
}