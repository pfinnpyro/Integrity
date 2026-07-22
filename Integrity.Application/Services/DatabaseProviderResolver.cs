using Integrity.Application.Interfaces;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;

namespace Integrity.Application.Services;

public class DatabaseProviderResolver
    : IDatabaseProviderResolver
{
    private readonly IEnumerable<IDatabaseProvider> _providers;
    
    public DatabaseProviderResolver(IEnumerable<IDatabaseProvider> providers)
    {
        _providers = providers;
    }
    
    public OperationResult<IDatabaseProvider> Resolve(DatabaseProvider provider)
    {
        var context = new OperationContext
        {
            EntityType = nameof(provider)
        };
        var connectionProvider = _providers
            .FirstOrDefault(x => x.Provider == provider);

        if (connectionProvider is null)
        {
            return OperationResult<IDatabaseProvider>.Failure(context,
                new Error("DCPR-NR", "DatabaseConnectionProviderResolver", ErrorType.Configuration, "Connection Provider does not exist on system"));
        }

        return OperationResult<IDatabaseProvider>.Success(context, connectionProvider);
    }
}