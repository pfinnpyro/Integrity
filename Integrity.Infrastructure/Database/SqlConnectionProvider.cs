using Integrity.Application.Interfaces;
using Integrity.Application.Models;
using Integrity.Infrastructure.Configuration;
using Microsoft.Data.SqlClient;

namespace Integrity.Infrastructure.Database;

public class SqlConnectionProvider : IDatabaseConnectionProvider
{
    private readonly ConnectionProfile _profile;

    public SqlConnectionProvider(ConnectionProfile profile)
    {
        _profile = profile;
    }
    
    public string GetConnectionString()
    {
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = _profile.Server,
            InitialCatalog = _profile.Database,
            UserID = _profile.Username,
            IntegratedSecurity = _profile.IntegratedSecurity,

        };
        
        return builder.ConnectionString;
    }

    public bool HasConnection()
    {
        return !string.IsNullOrEmpty(_profile.Server);
    }
}