namespace Integrity.Application.Models;

public sealed class OperationContext
{
    public Guid CorrelationId { get; init; } = Guid.NewGuid();
    
    public string Operation { get; init; } = string.Empty;
    public Guid? EntityId { get; init; }
    public string? EntityType { get; init; }
}