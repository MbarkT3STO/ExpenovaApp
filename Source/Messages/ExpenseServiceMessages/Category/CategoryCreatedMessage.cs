namespace Messages.ExpenseServiceMessages.Category;

public class CategoryCreatedMessage
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string Description { get; set; }
	public string UserId { get; set; }
}