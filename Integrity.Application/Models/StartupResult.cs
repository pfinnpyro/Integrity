using Integrity.Application.Models;

namespace Integrity.Application.Models;

public sealed class StartupResult
{
    public StartupStage Stage { get; }

    public StartupResult(StartupStage stage)
    {
        Stage = stage;
    }
}