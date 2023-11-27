using Messages.Interfaces;

namespace Messages.ExpenseServiceMessages.Category;

/// <summary>
/// Represents a message that is sent via RabbitMQ when a category is deleted.
/// </summary>
public class CategoryDeletedMessage: BaseEventMessage
{
	public required Guid Id { get; set; }
	public required string Name { get; set; }
	public required string Description { get; set; }
	public required string UserId { get; set; }

	public required DateTime CreatedAt { get; set; }
	public required string CreatedBy { get; set; }

	public required DateTime UpdatedAt { get; set; }
	public required string UpdatedBy { get; set; }

	public required DateTime DeletedAt { get; set; }
	public required string DeletedBy { get; set; }
}
