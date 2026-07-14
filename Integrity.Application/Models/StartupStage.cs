namespace Integrity.Application.Models;

public enum StartupStage
{
    Loading,
    ConfigurationRequired,
    AuthenticationRequired,
    Launcher,
    Shell

}