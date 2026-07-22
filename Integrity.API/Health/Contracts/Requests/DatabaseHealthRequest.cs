using Integrity.Application.Models.Configuration;

namespace Integrity.API.Health.Contracts.Requests;

public class DatabaseHealthRequest
{
    public ConnectionProfile Profile { get; set; }
    public string Password { get; set; }
}