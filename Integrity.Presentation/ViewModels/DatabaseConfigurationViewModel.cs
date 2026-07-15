using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Integrity.Application.Interfaces;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Presentation.Navigation;

namespace Integrity.Presentation.ViewModels;

public partial class DatabaseConfigurationViewModel(
    IConnectionProfileService _connectionProfileService) 
: ViewModelBase
{
    [ObservableProperty] private string? name;
    [ObservableProperty] private string? server;
    [ObservableProperty] private string? database;
    [ObservableProperty] private string? username;
    [ObservableProperty] private string? password;
    [ObservableProperty] private bool integratedSecurity;
    [ObservableProperty] private bool trustCertificate;

    [ObservableProperty] private string? _connectionStatus;
    
    [ObservableProperty] private List<Error> _errors = [];

    [RelayCommand]
    private async Task SaveAsync()
    {
        var profile = new ConnectionProfile
        {
            Server = Server,
            Database = Database,
            Username = Username,
            TrustCertificate = TrustCertificate,
            IntegratedSecurity = IntegratedSecurity
        };

        var result = _connectionProfileService.ValidateConnectionProfile(profile);

        if (!result.IsSuccess)
        {
            Errors = result.Errors;
            return;
        }

        var test = await _connectionProfileService.TestConnectionAsync(profile, password);

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
            Server = Server,
            Database = Database,
            Username = Username,
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

}