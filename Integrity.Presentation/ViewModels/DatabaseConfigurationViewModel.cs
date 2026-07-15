using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Integrity.Application.Interfaces;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Presentation.Navigation;

namespace Integrity.Presentation.ViewModels;

public partial class DatabaseConfigurationViewModel : ViewModelBase
{
    private readonly IConnectionProfileService _connectionProfileService;
    private readonly INavigationService _navigationService;

    [ObservableProperty] private string? _name;
    [ObservableProperty] private string? _server;
    [ObservableProperty] private string? _database;
    [ObservableProperty] private string? _username;
    [ObservableProperty] private string? _password;

    [ObservableProperty] private List<Error> _errors = [];
    
    
    public DatabaseConfigurationViewModel(
        IConnectionProfileService connectionProfileService,
        INavigationService navigationService)
    {
        _connectionProfileService = connectionProfileService;
        _navigationService = navigationService;
    }

    [RelayCommand]
    private async Task SaveAsync()
    {
        var profile = new ConnectionProfile
        {
            Id = Guid.NewGuid(),
            Name = Name,
            Server = Server,
            Database = Database,
            Username = Username
        };


        var credentials = new ConnectionCredentials
        {
            Password = Password
        };
        
        var result = await _connectionProfileService.TestConnectionAsync(profile);
        
        if (!result.IsSuccess)
        {
            Errors = result.Errors.ToList();
            return;
        }
        
        await _connectionProfileService.SaveConnectionProfileAsync(profile);

        await _navigationService.NavigateToAsync<DatabaseConfigurationViewModel>();
    }
    
}