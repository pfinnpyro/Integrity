using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;

namespace Integrity.Application.Models;

public record StartupResult(StartupStage Stage, ConnectionProfile? Profile = null);
