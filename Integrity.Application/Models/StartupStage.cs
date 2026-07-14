namespace Integrity.Application.Models.Results;

public enum StartupStage
{
    Loading,
    ConfigurationRequired,
    AuthenticationRequired,
    Launcher,
    Shell

}