namespace Integrity.Application.Models;

public class OperationContext
{
    public Guid CorrelationId { get; init; } = Guid.NewGuid();
    public Guid? EntityId { get; init; }
    public string? EntityType { get; init; }
}