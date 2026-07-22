namespace Integrity.Application.Models;

public enum StartupStage
{
    Loading,
    ConfigurationRequired,
    DatabaseAuthenticationRequired,
    AuthenticationRequired,
    Launcher,
    Shell,
    ApiError
}