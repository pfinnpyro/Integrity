namespace Integrity.Application.Models.Types;

public enum ErrorType
{
    Configuration,
    Validation,
    BusinessRule,
    Infrastructure,
    Security,
    Unknown,
    NonExecution
}