using Integrity.Application.Models;

namespace Integrity.Application.Services;

public interface IApplicationHost
{
    Task<StartupResult> StartAsync();
}