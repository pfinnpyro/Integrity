using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Integrity.Application.Interfaces;
using Integrity.Application.Models;
using Integrity.Application.Models.Configuration;
using Integrity.Presentation.ViewModels;

namespace Integrity.Views;

public partial class DatabaseConfigurationView(
    IConnectionProfileService _connectionProfileService) 
    : ViewModelBase
{
    [ObservableProperty] private string? server;
    [ObservableProperty] private string? database;
    [ObservableProperty] private string? username;
    [ObservableProperty] private string? password;
    
    [ObservableProperty] private List<Error> _errors = [];
    
    [RelayCommand]
    private async Task SaveAsync()
    {
        var profile = new ConnectionProfile
        {
            Server = Server,
            Database = Database,
            Username = Username
        };

        var result = _connectionProfileService.ValidateConnectionProfile(profile);
        
        if (!result.IsSuccess)
        {
            Errors = result.Errors;
            return;
        }
        
        await _connectionProfileService.SaveConnectionProfileAsync(profile);
    }
}