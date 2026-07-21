using Integrity.Application.Interfaces;
using System.Text.Json;
using Integrity.Application.Models;
using Integrity.Infrastructure.Database;
using Integrity.Application.Models.Configuration;
using Integrity.Application.Models.Types;

namespace Integrity.Infrastructure.Configuration;

public class JsonConnectionProfileStore : IConnectionProfileStore
{

    
    private readonly string _connectionProfileFilePath;
    private readonly SqlConnectionProvider _connectionProvider;

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
    public async Task<OperationResult> HasConnectionProfilesAsync()
    {
        var context = new OperationContext
        {
            EntityType = nameof(ConnectionProfile)
        };

        try
        {
            var profiles = await GetAllConnectionProfilesAsync();

            foreach (var profile in profiles.Value)
            {
                var validation = _connectionProvider.ValidateConnectionProfile(profile);
                if (!validation.IsSuccess)
                {
                    return OperationResult.Failure(context, validation.Errors.ToArray());
                }
            }
            
            if ( !profiles.IsSuccess )
            {
                return OperationResult.Failure(context, profiles.Errors.ToArray());
            }
            
            if ( profiles.Value.Count == 0 )
            {
                return OperationResult.Failure(context, new Error(
                    "INTERNAL",
                    "JsonConnectionProfileStore",
                    ErrorType.Configuration,
                    "No connection profiles found"));
            }
        }
        catch ( Exception ex)
        {
            return OperationResult.Failure(context, new Error(
                "INTERNAL",
                "JsonConnectionProfileStore",
                ErrorType.Infrastructure,
                ex.Message));
        }
        return OperationResult.Success(context);
        
    }

    public async Task<OperationResult<Guid>> SaveConnectionProfileAsync(ConnectionProfile profile)
    {
        var existingProfiles = await GetAllConnectionProfilesAsync();
        
        if(!existingProfiles.IsSuccess)
        {
            return existingProfiles.ToFailure<Guid>();
        }
        
        var existingProfileIndex = existingProfiles.Value.FindIndex(x => x.Id == profile.Id);
        
        if(existingProfileIndex != -1)
        {
            existingProfiles.Value[existingProfileIndex] = profile;
        }
        else
        {
            existingProfiles.Value.Add(profile);
        }

        var json = JsonSerializer.Serialize(existingProfiles.Value, new JsonSerializerOptions { WriteIndented = true });
        try
        {
            await File.WriteAllTextAsync(_connectionProfileFilePath, json);
        }
        catch(Exception ex)
        {
            return OperationResult<Guid>.Failure(existingProfiles.Context, 
                new Error("INTERNAL", "JsonConnectionProfileStore", ErrorType.Infrastructure, ex.Message));
        }

        return OperationResult<Guid>.Success(existingProfiles.Context, profile.Id );
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

    public async Task<OperationResult<List<ConnectionProfile>>> GetAllConnectionProfilesAsync()
    {
        var context = new OperationContext
        {
            EntityType = nameof(ConnectionProfile)
        };

        try
        {
            if(File.Exists(_connectionProfileFilePath))
            {
                var json = await File.ReadAllTextAsync(_connectionProfileFilePath);
                var profiles = JsonSerializer.Deserialize<List<ConnectionProfile>>(json)
                       ?? [];
                return OperationResult<List<ConnectionProfile>>.Success(context, profiles);
            }
        }
        catch(Exception ex)
        {
            return OperationResult<List<ConnectionProfile>>.Failure(context, 
                new Error("INTERNAL", "JsonConnectionProfileStore", ErrorType.Infrastructure, ex.Message));
        }

        return OperationResult<List<ConnectionProfile>>.Success(context, 
            []);
    }

    public async Task<OperationResult> DeleteConnectionProfileAsync(Guid profileId)
    {
        var context = new OperationContext
        {
            EntityId = profileId,
            EntityType = nameof(ConnectionProfile),
        };
        
        var existingProfiles = await GetAllConnectionProfilesAsync();
        
        if(!existingProfiles.IsSuccess)
        {
            return OperationResult<Unit>.Failure(context, existingProfiles.Errors.ToArray());

        }

        existingProfiles.Value.RemoveAll(x => x.Id == profileId);
        var json = JsonSerializer.Serialize(existingProfiles, new JsonSerializerOptions { WriteIndented = true });
        await File.WriteAllTextAsync(_connectionProfileFilePath, json);

        return OperationResult.Success(context);
    }
    
    public async Task<OperationResult> SetActiveConnectionProfileAsync(Guid profileId)
    {
        var context = new OperationContext
        {
            EntityId = profileId,
            EntityType = nameof(ConnectionProfile),
        };
        try
        {
            var existingProfiles = await GetAllConnectionProfilesAsync();
            var targetProfile = existingProfiles.Value.FirstOrDefault(x => x.Id == profileId);
            if(targetProfile == null)
            {
                throw new InvalidOperationException("Profile not found");
            }
            foreach (var profile in existingProfiles.Value)
            {
                profile.IsActive = profile.Id == profileId;
            }

            var json = JsonSerializer.Serialize(existingProfiles, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(_connectionProfileFilePath, json);
        }
        catch (Exception ex)
        {
            return OperationResult<Unit>.Failure(context, new Error(
                "INTERNAL",
                "JsonConnectionProfileStore",
                ErrorType.Infrastructure,
                ex.Message));
        }
        
        return OperationResult.Success(context);
    }
}