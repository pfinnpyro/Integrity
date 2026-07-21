using System.Runtime.CompilerServices;
using Integrity.Application.Models.Types;

namespace Integrity.Application.Models;

/**
 * TODO: Restructure OperationContext funcationality to allow responsibility-centered traces segregated by Operation.
 *    Will require implementation of separate class function for assigning parent operations and breaking appropriately.
 */
public class OperationResult
{
    public bool IsSuccess { get; protected init; }

    public OperationContext Context { get; init; } = null!;

    public List<Error> Errors { get; init; } = [];

    public static OperationResult Success(OperationContext context)
    {
        return new()
        {
            IsSuccess = true,
            Context = context
        };
    }

    public static OperationResult Failure(
        OperationContext context,
        params Error[] errors)
    {
        return new()
        {
            IsSuccess = false,
            Context = context,
            Errors = errors.ToList()
        };
    }

    public OperationResult ToFailure()
    {
        return Failure(Context!, Errors.ToArray());
    }
}

public sealed class OperationResult<T> : OperationResult
{
    public T? Value { get; init; }

    public static OperationResult<T> Success(OperationContext context, T value)
    {
        return new()
        {
            IsSuccess = true,
            Value = value,
            Context = context
        };
    }

    public new static OperationResult<T> Failure(
        OperationContext context,
        params Error[] errors)
    {
        return new()
        {
            IsSuccess = false,
            Context = context,
            Errors = errors.ToList()
        };
    }
    
    public OperationResult<TNew> ToFailure<TNew>()
    {
        return OperationResult<TNew>.Failure(Context!, Errors.ToArray());
    }
}