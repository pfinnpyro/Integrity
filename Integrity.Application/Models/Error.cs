using Integrity.Application.Models.Types;

namespace Integrity.Application.Models;

public record Error(string Code, string Service, Types.ErrorType Type, string Message);

public class Errors
{
    public static readonly Error InvalidServer = 
        new("001",
            "Connection Profile Service",
            Types.ErrorType.Validation,
            "Invalid Server");
    
    public static readonly Error InvalidDatabase =
        new("002",
            "Connection Profile Service",
            Types.ErrorType.Validation,
            "Invalid Database");
    
    public static readonly Error InvalidName =
        new("003",
            "Connection Profile Service",
            Types.ErrorType.Validation,
            "Invalid Name");

    public static readonly Error InvalidPassword =
        new("004",
            "Connection Profile Service",
            Types.ErrorType.Validation,
            "Invalid Password");
    
    public static readonly Error InvalidProvider =
        new("005",
            "Connection Profile Service",
            Types.ErrorType.Validation,
            "Invalid Provider");
}