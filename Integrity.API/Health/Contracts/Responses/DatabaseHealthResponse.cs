using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;

namespace Integrity.API.Health.Contracts.Responses;

public sealed record DatabaseHealthResponse
(
    HealthStatus HealthStatus,
    DatabaseMetadata Metadata,
    long? ResponseTimeMilliseconds,
    IReadOnlyList<Error> Warnings
    //TODO: string? DatabaseVersion / SchemaVersion
);