using System.Data.Common;
using System.Security;
using Integrity.Application.Interfaces;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;
using Integrity.Infrastructure.Configuration;
using Microsoft.Data.SqlClient;

namespace Integrity.Infrastructure.Database;

public class SqlDatabaseProvider : IDatabaseProvider
{

    public DatabaseProvider Provider => DatabaseProvider.MsSqlServer;
    public string GetConnectionString(ConnectionProfile profile, string password)
    {
        var builder = new SqlConnectionStringBuilder
        {
            DataSource = profile.Server,
            InitialCatalog = profile.Database,
            TrustServerCertificate = profile.TrustCertificate,
            IntegratedSecurity = profile.IntegratedSecurity,
        };
        
        if(!profile.IntegratedSecurity)
        {
            builder.UserID = profile.Username;
            builder.Password = password;
        }
        
        return builder.ConnectionString;
    }

    public async Task<OperationResult<DatabaseMetadata>> CheckHealthAsync(ConnectionProfile profile, string password)
    {
        var context = new OperationContext
        {
            EntityId = profile.Id,
            EntityType = nameof(ConnectionProfile)
        };
        
        try
        {
            var validation = ValidateConnectionProfile(profile);
            
            if (validation.IsSuccess)
            {
                var connectionString = GetConnectionString(profile, password);
                await using var connection = new SqlConnection(connectionString);
                await connection.OpenAsync();
                
                return OperationResult<DatabaseMetadata>.Success(context, new DatabaseMetadata
                (
                    nameof(SqlDatabaseProvider),
                    connection.ServerVersion,
                    connection.Database
                ));
            }
            return validation.ToFailure<DatabaseMetadata>();
            
        }
        catch (SqlException ex)
        {
            return OperationResult<DatabaseMetadata>.Failure(context, new Error(
                "1001",
                nameof(SqlDatabaseProvider),
                ErrorType.Infrastructure,
                ex.Message
                ));
        }
        catch (Exception ex)
        {
            return OperationResult<DatabaseMetadata>.Failure(context, new Error(
                "1002",
                nameof(SqlDatabaseProvider),
                ErrorType.Infrastructure,
                ex.Message
                ));
        }
    }
    
    public OperationResult ValidateConnectionProfile(ConnectionProfile profile)
    {
        var context = new OperationContext
        {
            EntityId = profile.Id,
            EntityType = nameof(ConnectionProfile)
        };
        
        var errors = new List<Error>();
        
        if(string.IsNullOrEmpty(profile.Name))
            errors.Add(Errors.InvalidName);
        if (string.IsNullOrEmpty(profile.Server))
            errors.Add(Errors.InvalidServer);
        if (string.IsNullOrEmpty(profile.Database))
            errors.Add(Errors.InvalidDatabase);
        
        if(errors.Count > 0)
            return OperationResult.Failure(context, errors.ToArray());

        return OperationResult.Success(context);
    }
    
    public Task<DatabaseMetadata> GetMetadataAsync()
    {
        throw new NotImplementedException();
    }
    
    public async Task<DbConnection> OpenConnectionAsync()
    {
        // TODO: OpenAsync from **what**? We need a ConnectionString pulled in from somewhere
        var connection = new SqlConnection();
        
        await connection.OpenAsync();

        return connection;
    }
}