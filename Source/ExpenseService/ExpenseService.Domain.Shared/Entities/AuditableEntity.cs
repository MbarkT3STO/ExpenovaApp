using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Domain.Shared.Entities;

/// <summary>
/// Represents the base auditable entity.
/// </summary>
public abstract class AuditableEntity<T>: Entity<T>, IAuditableEntity
{
	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? LastUpdatedAt { get; set; }
	public string LastUpdatedBy { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public string DeletedBy { get; set; }
}
