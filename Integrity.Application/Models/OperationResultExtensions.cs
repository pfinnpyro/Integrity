namespace Integrity.Application.Models;

public static class OperationResultExtensions
{
    public static OperationResult<T> ToFailure<T>(this OperationResult result)
    {
        return OperationResult<T>.Failure(
            result.Context!,
            result.Errors.ToArray());
    }
}