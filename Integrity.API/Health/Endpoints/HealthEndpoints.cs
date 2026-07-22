using Integrity.API.Health.Contracts;
using Integrity.API.Health.Contracts.Requests;
using Integrity.API.Health.Responses;
using Integrity.API.Health.Services;
using Microsoft.AspNetCore.Mvc;

namespace Integrity.API.Health.Endpoints;

public static class HealthEndpoints
{
   
   public static void MapHealthEndpoints(this WebApplication app)
   {
      app.MapGet(
         "/api/health/live", () => Results.Ok("Success")
      );
      app.MapGet(
         "/api/health/ready",
         async (
            [FromServices] DatabaseHealthService healthService,
            [FromBody] DatabaseHealthRequest request) =>
         {
            var response = await healthService.CheckDatabaseAsync(
               request.Profile,
               request.Password);
            
            switch (response.HealthStatus)
            {
               case HealthStatus.Healthy:
                  return new HealthResponse(
                     HealthStatus.Healthy,
                     DateTimeOffset.UtcNow,
                     "0_1",
                     response
                  );
               case HealthStatus.Degraded:
                  return new HealthResponse(
                     HealthStatus.Degraded,
                     DateTimeOffset.UtcNow,
                     "0_1",
                     response
                  );
               default:
               case HealthStatus.Unhealthy:
                  return new HealthResponse(
                     HealthStatus.Unhealthy,
                     DateTimeOffset.UtcNow,
                     "0_1",
                     response
                  );
            }
         });
   }
}