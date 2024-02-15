namespace ExpenseService.Application.Shared;

/// <summary>
/// Represents an auditable data transfer object.
/// </summary>
public class AuditableDTO
{
	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? LastUpdatedAt { get; set; }
	public string LastUpdatedBy { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public string? DeletedBy { get; set; }
}
