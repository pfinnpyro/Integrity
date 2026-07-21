using System.Security;
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

    public async Task<OperationResult> TestConnectionAsync(ConnectionProfile profile, string password)
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
                return OperationResult.Success(context);
            }
            return validation.ToFailure();
            
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
}