using Integrity.Application.Models;

namespace Integrity.Application.Services;

public interface IStartupService
{
    Task<StartupResult> InitializeAsync();
}