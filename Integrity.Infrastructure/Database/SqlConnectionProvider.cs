using Integrity.Application.Interfaces;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;
using Integrity.Infrastructure.Configuration;
using Microsoft.Data.SqlClient;

namespace Integrity.Infrastructure.Database;

public class SqlConnectionProvider : IDatabaseConnectionProvider
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

    public async Task<OperationResult<Unit>> TestConnectionAsync(ConnectionProfile profile, string password)
    {
        var context = new OperationContext
        {
            EntityId = profile.Id,
            EntityType = nameof(ConnectionProfile)
        };
        
        try
        {
            var connectionString = GetConnectionString(profile, password);
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();
            return OperationResult.Success();
        }
        catch (SqlException ex)
        {
            return OperationResult<Unit>.Failure(context, new Error(
                "1001",
                nameof(SqlConnectionProvider),
                ErrorType.Infrastructure,
                ex.Message
                ));
        }
        catch (Exception ex)
        {
            return OperationResult<Unit>.Failure(context, new Error(
                "1002",
                nameof(SqlConnectionProvider),
                ErrorType.Infrastructure,
                ex.Message
                ));
        }
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
}