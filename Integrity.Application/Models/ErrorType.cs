namespace Integrity.Application.Models;

public enum ErrorType
{
    Validation,
    BusinessRule,
    Infrastructure,
    Security,
    Unknown
}