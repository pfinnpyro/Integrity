using Integrity.Application.Interfaces;

namespace Integrity.Infrastructure.Database;

public class IntegrityDbContext: DbContext
{
    private readonly IDatabaseConnectionProvider _provider;

    protected override void OnConfiguring(
        DbContextOptionsBuilder options)
    {
        options.UseSqlServer(_provider.GetConnectionString());
    }
}