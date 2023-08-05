namespace ExpenseService.Domain.Shared.Models;

/// <summary>
/// Represents the base auditable entity.
/// </summary>
public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
{
	public DateTime CreatedDate { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? LastUpdatedDate { get; set; }
	public string LastUpdatedBy { get; set; }
	public bool IsDeleted { get; set; }
}
