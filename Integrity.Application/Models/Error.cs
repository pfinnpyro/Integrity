namespace Integrity.Application.Models;

public record Error(string Code, string Service, ErrorType Type, string Message);

public class Errors
{
    public static readonly Error InvalidServer = 
        new("001",
            "Connection Profile Service",
            ErrorType.Validation,
            "Invalid Server");
    
    public static readonly Error InvalidDatabase =
        new("002",
            "Connection Profile Service",
            ErrorType.Validation,
            "Invalid Database");
    
    public static readonly Error InvalidName =
        new("003",
            "Connection Profile Service",
            ErrorType.Validation,
            "Invalid Name");
}