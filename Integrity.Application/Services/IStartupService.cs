using Integrity.Application.Models;
using Integrity.Application.Models.Results;

namespace Integrity.Application.Services;

public interface IStartupService
{
    Task<StartupResult> InitializeAsync();
}