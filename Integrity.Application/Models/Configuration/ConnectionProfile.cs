namespace Integrity.Application.Models.Configuration;

public sealed class ConnectionProfile
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Name { get; set; } = "Default";
    
    public string Server { get; set; } = string.Empty;
    
    public string Database { get; set; } = string.Empty;

    public string Username { get; set; } = string.Empty;

    public bool IntegratedSecurity { get; set; }
    
    public bool TrustCertificate { get; set; }

    public bool IsActive { get; set; } = false;
    
    public DatabaseProvider Provider { get; set; } = DatabaseProvider.MsSqlServer;
}