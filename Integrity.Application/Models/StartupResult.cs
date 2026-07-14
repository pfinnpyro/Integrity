using Integrity.Application.Models.Results;

namespace Integrity.Application.Models;

public sealed class StartupResult
{
    public StartupStage Stage { get; }

    public StartupResult(StartupStage stage)
    {
        Stage = stage;
    }
}