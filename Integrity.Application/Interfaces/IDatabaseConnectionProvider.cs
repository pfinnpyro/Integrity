namespace Integrity.Application.Interfaces;

public interface IDatabaseConnectionProvider
{
    string GetConnectionString();
    bool HasConnection();
}