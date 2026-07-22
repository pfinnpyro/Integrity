using System.Diagnostics;
using Integrity.API.Health.Contracts;
using Integrity.API.Health.Contracts.Responses;
using Integrity.Application.Interfaces;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;

namespace Integrity.API.Health.Services;

public class DatabaseHealthService (IDatabaseProvider databaseProvider)
{
    public async Task<DatabaseHealthResponse> CheckDatabaseAsync(
        ConnectionProfile profile,
        string password)
    {
        var stopwatch = Stopwatch.StartNew();

        var result = await databaseProvider.CheckHealthAsync(profile, password);

        stopwatch.Stop();

        if (!result.IsSuccess)
        {
            return new DatabaseHealthResponse(
                HealthStatus.Unhealthy,
                null,
                stopwatch.ElapsedMilliseconds,
                result.Errors);
        }

        var metadata = result.Value!;

        var warnings = new List<Error>();

        var status = HealthStatus.Healthy;

        if (stopwatch.ElapsedMilliseconds > 500)
        {
            status = HealthStatus.Degraded;

            warnings.Add(new Error(
                "HIGH_LATENCY",
                "Database Health Check",
                ErrorType.Infrastructure,
                "Database response exceeded expected threshold."));
        }

        return new DatabaseHealthResponse(
            status,
            metadata,
            stopwatch.ElapsedMilliseconds,
            warnings);
    }
}