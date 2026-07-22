namespace Integrity.Application.Interfaces;

public interface IDatabaseMetadataProvider
{
    string DatabaseName { get; }
    Task<string> GetDatabaseNameAsync();
    Task<string> GetDatabaseVersionAsync();
}