using Integrity.Application.Models.Configuration;

namespace Integrity.Application.Interfaces;

public interface IConnectionProfileStore
{ 
    Task<List<ConnectionProfile>> GetAllConnectionProfilesAsync();

    Task<ConnectionProfile?> GetConnectionProfileAsync(Guid profileId);
    
    Task<ConnectionProfile?> GetActiveConnectionProfileAsync();
    
    Task<Guid> SaveConnectionProfileAsync(ConnectionProfile profile);
    
    Task SetActiveConnectionProfileAsync(Guid profileId);
    Task<bool> HasConnectionProfileAsync();
    Task DeleteConnectionProfile(Guid profileId);
}