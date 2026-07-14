using Integrity.Application.Models;

namespace Integrity.Application.Interfaces;

public interface IConnectionProfileStore
{ 
    Task<ConnectionProfile?> GetConnectionProfile();
    Task SaveConnectionProfile(ConnectionProfile profile);
    Task<bool> HasConnectionProfile();
    Task DeleteConnectionProfile();
}