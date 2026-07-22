using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;

namespace Integrity.Application.Interfaces;

public interface IDatabaseProviderResolver
{
    OperationResult<IDatabaseProvider> Resolve(DatabaseProvider provider);
}