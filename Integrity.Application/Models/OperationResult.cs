using System.Runtime.CompilerServices;

namespace Integrity.Application.Models;

public sealed class OperationResult<T>
{
    public bool IsSuccess { get; private set; }
    public T? Value { get; init; }
    
    public OperationContext? Context { get; init; }
    public List<Error> Errors { get; init; } = [];

    public void AddError(Error error)
    {
        Errors.Add(error);
        IsSuccess = false;
    }

    public static OperationResult<T> Failure(
        OperationContext context,
        params Error[] errors)
    {
        return new OperationResult<T>
        {
            IsSuccess = false,
            Context = context,
            Errors = errors.ToList()
        };
    }
    
    public static OperationResult<T> Success(T value)
    {
        
        return new OperationResult<T>
        {
            IsSuccess = true,
            Value = value
        };
    }
}

public static class OperationResult
{
    public static OperationResult<Unit> Success() =>
        OperationResult<Unit>.Success(Unit.Value);
}