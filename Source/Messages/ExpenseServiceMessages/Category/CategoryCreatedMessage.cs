namespace Messages.ExpenseServiceMessages.Category;

public class CategoryCreatedMessage : BaseEventMessage
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string UserId { get; set; }
	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
}
