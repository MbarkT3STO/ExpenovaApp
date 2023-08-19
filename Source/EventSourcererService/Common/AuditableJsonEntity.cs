namespace EventSourcererService.Common;

/// <summary>
/// Represents an abstract class for auditable JSON entities.
/// </summary>
/// <typeparam name="T">The type of the unique identifier.</typeparam>
public abstract class AuditableJsonEntity<T> : JsonEntity<T>
{
	public DateTime CreatedDate { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? LastUpdatedDate { get; set; }
	public string LastUpdatedBy { get; set; }
	public bool IsDeleted { get; set; }
}
