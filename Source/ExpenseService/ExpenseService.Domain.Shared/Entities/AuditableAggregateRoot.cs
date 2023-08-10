namespace ExpenseService.Domain.Shared.Entities;

/// <summary>
/// Represents the base auditable aggregate root.
/// </summary>
public abstract class AuditableAggregateRoot<T> : AuditableEntity<T>, IAggregateRoot
{
	
}
