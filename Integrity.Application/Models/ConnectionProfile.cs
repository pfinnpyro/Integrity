namespace Integrity.Application.Models;

public class ConnectionProfile
{
    public Guid Id { get; set; }
    
    public string Name { get; set; } = "Default";
    
    public string Server { get; set; } = string.Empty;
    
    public string Database { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public bool IntegratedSecurity { get; set; }
}