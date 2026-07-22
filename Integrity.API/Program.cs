using Integrity.API.Health.Endpoints;
using Integrity.API.Health.Services;

namespace Integrity.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.Build();
        app.MapHealthEndpoints();
        app.Run();
    }
}