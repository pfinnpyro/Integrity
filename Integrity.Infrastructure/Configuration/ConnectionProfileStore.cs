using Integrity.Application.Interfaces;
using Integrity.Application.Models;

namespace Integrity.Infrastructure.Configuration;

public interface ConnectionProfileStore : IConnectionProfileStore
{
    ConnectionProfile? Load();
    void Save(ConnectionProfile connectionProfile);
}