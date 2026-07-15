using Integrity.Application.Interfaces;
using System.Text.Json;
using Integrity.Application.Models.Configuration;

namespace Integrity.Infrastructure.Configuration;

public class JsonConnectionProfileStore : IConnectionProfileStore
{

    
    private readonly string _connectionProfileFilePath;

    public JsonConnectionProfileStore()
    {
        var appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        var profileDirectory = Path.Combine(
            appDataPath,
            "Integrity",
            "Profiles"
        );
        
        Directory.CreateDirectory(profileDirectory);
        
        _connectionProfileFilePath = Path.Combine(profileDirectory, "ConnectionProfiles.json");
}
    //TODO: Rename to HasConnectionProfilesAsync and handle verifying that one or more profiles exist
    public Task<bool> HasConnectionProfileAsync()
    {
        return Task.FromResult(File.Exists(_connectionProfileFilePath));
    }

    public async Task<Guid> SaveConnectionProfileAsync(ConnectionProfile profile)
    {
        var existingProfiles = await GetAllConnectionProfilesAsync();
        
        var existingProfileIndex = existingProfiles.FindIndex(x => x.Id == profile.Id);
        
        if(existingProfileIndex != -1)
        {
            existingProfiles[existingProfileIndex] = profile;
        }
        else
        {
            existingProfiles.Add(profile);
        }

        var json = JsonSerializer.Serialize(existingProfiles, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_connectionProfileFilePath, json);

        return profile.Id;
    }
    
    public async Task<ConnectionProfile?> GetActiveConnectionProfileAsync()
    {
        if(File.Exists(_connectionProfileFilePath))
        {
            var json = await File.ReadAllTextAsync(_connectionProfileFilePath);
            var profiles = JsonSerializer.Deserialize<List<ConnectionProfile>>(json)
                           ?? new List<ConnectionProfile>();

            return profiles.FirstOrDefault(x => x.IsActive);
        }

        return null;
    }

    public async Task<ConnectionProfile?> GetConnectionProfileAsync(Guid profileId)
    {
        if(File.Exists(_connectionProfileFilePath))
        {
            var json = await File.ReadAllTextAsync(_connectionProfileFilePath);
            var profiles = JsonSerializer.Deserialize<List<ConnectionProfile>>(json)
                           ?? [];

            return profiles.FirstOrDefault(x => x.Id == profileId);
        }
        
        return null;
    }

    public async Task<List<ConnectionProfile>> GetAllConnectionProfilesAsync()
    {
        if(File.Exists(_connectionProfileFilePath))
        {
            var json = await File.ReadAllTextAsync(_connectionProfileFilePath);
            return JsonSerializer.Deserialize<List<ConnectionProfile>>(json)
                   ?? [];
        }

        return [];
    }

    public async Task DeleteConnectionProfileAsync(Guid profileId)
    {
        var existingProfiles = await GetAllConnectionProfilesAsync();
        existingProfiles.RemoveAll(x => x.Id == profileId);
        var json = JsonSerializer.Serialize(existingProfiles, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_connectionProfileFilePath, json);
    }
    
    public async Task SetActiveConnectionProfileAsync(Guid profileId)
    {
        var profiles = await GetAllConnectionProfilesAsync();
        var targetProfile = profiles.FirstOrDefault(x => x.Id == profileId);
        if(targetProfile == null)
        {
            throw new InvalidOperationException("Profile not found");
        }
        foreach (var profile in profiles)
        {
            profile.IsActive = profile.Id == profileId;
        }

        var json = JsonSerializer.Serialize(profiles, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_connectionProfileFilePath, json);
    }
}