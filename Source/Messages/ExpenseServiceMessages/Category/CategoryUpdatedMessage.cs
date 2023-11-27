namespace Messages.ExpenseServiceMessages.Category;

/// <summary>
/// Represents a message that is sent via RabbitMQ when a category is updated.
/// </summary>
public class CategoryUpdatedMessage : BaseEventMessage
{
	public Guid Id { get; set; }
	public string NewName { get; set; }
	public string NewDescription { get; set; } 
	public string UserId { get; set; } 
	
	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
	
	public DateTime UpdatedAt { get; set; }
	public string UpdatedBy { get; set; }
}
