namespace Messages.ExpenseServiceMessages.Category;

/// <summary>
/// Represents a message that is sent via RabbitMQ when a category is deleted.
/// </summary>
public class CategoryDeletedMessage
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string UserId { get; set; }

	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }

	public DateTime UpdatedAt { get; set; }
	public string UpdatedBy { get; set; }
	
	public DateTime DeletedAt { get; set; }
	public string DeletedBy { get; set; }
}
