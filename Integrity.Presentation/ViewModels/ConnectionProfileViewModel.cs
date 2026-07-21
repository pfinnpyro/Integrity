using System.Collections.ObjectModel;
using System.Security;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Integrity.Application.Interfaces;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Presentation.Navigation;

namespace Integrity.Presentation.ViewModels;

public partial class ConnectionProfileViewModel(
    IConnectionProfileService _connectionProfileService) 
: ViewModelBase
{
    
    public ObservableCollection<ConnectionProfile> Profiles { get; }= [];
    [ObservableProperty] private ConnectionProfile? _selectedProfile;
    [ObservableProperty] private string? name;
    [ObservableProperty] private string? server;
    [ObservableProperty] private string? database;
    [ObservableProperty] private string? username;
    [ObservableProperty] private string? _password = null;
    [ObservableProperty] private DatabaseProvider provider = DatabaseProvider.MsSqlServer;
    [ObservableProperty] private bool integratedSecurity;
    [ObservableProperty] private bool trustCertificate;

    [ObservableProperty] private string? _connectionStatus;
    
    [ObservableProperty] private List<Error> _errors = [];
    
    public IEnumerable<DatabaseProvider> Providers => Enum.GetValues<DatabaseProvider>();

    [RelayCommand]
    private async Task SaveAsync()
    {
        var profile = SelectedProfile ?? new ConnectionProfile();
            
            profile.Name = Name;
            profile.Server = Server;
            profile.Database = Database;
            profile.Username = Username;
            profile.Provider = Provider;
            profile.TrustCertificate = TrustCertificate;
            profile.IntegratedSecurity = IntegratedSecurity;

        var result = _connectionProfileService.ValidateConnectionProfile(profile);

        if (!result.IsSuccess)
        {
            Errors = result.Errors;
            return;
        }

        var test = await _connectionProfileService.TestConnectionAsync(profile, _password);

        if (!test.IsSuccess)
        {
            Errors = test.Errors;
            return;
        }

        await _connectionProfileService.SaveConnectionProfileAsync(profile);
    }

    [RelayCommand]
    private async Task TestAsync()
    {
        
        var result = await _connectionProfileService.TestConnectionAsync(new ConnectionProfile
        {
            Name = Name,
            Server = Server,
            Database = Database,
            Username = Username,
            Provider = Provider,
            TrustCertificate = TrustCertificate,
            IntegratedSecurity = IntegratedSecurity
        }, Password);
        
        if (result.IsSuccess)
        {
            ConnectionStatus = "Connection successful";
        }
        else
        {
            ConnectionStatus = string.Join(Environment.NewLine, result.Errors.Select(e => e.Message));
        }
    }
    public async Task LoadProfilesAsync()
    {
        var result = await _connectionProfileService.GetAllConnectionProfilesAsync();

        if (!result.IsSuccess)
        {
            Errors.AddRange(result.Errors);
            return;
        }

        Profiles.Clear();

        foreach (var profile in result.Value)
        {
            Profiles.Add(profile);
        }
    }

    partial void OnSelectedProfileChanged(
        ConnectionProfile? value)
    {
        if (value is null)
        {
            return;
        }

        Name = value.Name;
        Server = value.Server;
        Database = value.Database;
        Username = value.Username;
        Provider = value.Provider;
        TrustCertificate = value.TrustCertificate;
        IntegratedSecurity = value.IntegratedSecurity;
    }
    
    [RelayCommand]
    private async Task RefreshProfilesAsync()
    {
        await LoadProfilesAsync();
    }

}