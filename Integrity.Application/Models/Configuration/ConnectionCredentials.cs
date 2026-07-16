using System.Security;

namespace Integrity.Application.Models.Configuration;

public class ConnectionCredentials
{
    public SecureString Password { get; set; } = string.Empty;
}