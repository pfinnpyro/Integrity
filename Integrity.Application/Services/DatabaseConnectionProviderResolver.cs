using Integrity.Application.Interfaces;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;

namespace Integrity.Application.Services;

public class DatabaseConnectionProviderResolver
    : IDatabaseConnectionProviderResolver
{
    private readonly IEnumerable<IDatabaseConnectionProvider> _providers;
    
    public DatabaseConnectionProviderResolver(IEnumerable<IDatabaseConnectionProvider> providers)
    {
        _providers = providers;
    }
    
    public OperationResult<IDatabaseConnectionProvider> Resolve(DatabaseProvider provider)
    {
        var context = new OperationContext
        {
            EntityType = nameof(provider)
        };
        var connectionProvider = _providers
            .FirstOrDefault(x => x.Provider == provider);

        if (connectionProvider is null)
        {
            return OperationResult<IDatabaseConnectionProvider>.Failure(context,
                new Error("DCPR-NR", "DatabaseConnectionProviderResolver", ErrorType.Configuration, "Connection Provider does not exist on system"));
        }

        return OperationResult<IDatabaseConnectionProvider>.Success(connectionProvider);
    }
}