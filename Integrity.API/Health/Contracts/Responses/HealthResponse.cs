using Integrity.API.Health;
using Integrity.API.Health.Contracts;
using Integrity.API.Health.Contracts.Responses;

namespace Integrity.API.Health.Responses;

public sealed record HealthResponse
(
    HealthStatus Status,
    DateTimeOffset Timestamp,
    string version,
    DatabaseHealthResponse DatabaseHealth
);