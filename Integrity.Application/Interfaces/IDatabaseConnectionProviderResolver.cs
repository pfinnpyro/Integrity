using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;

namespace Integrity.Application.Interfaces;

public interface IDatabaseConnectionProviderResolver
{
    OperationResult<IDatabaseConnectionProvider> Resolve(DatabaseProvider provider);
}