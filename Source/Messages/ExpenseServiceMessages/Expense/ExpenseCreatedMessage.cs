namespace Messages.ExpenseServiceMessages.Expense;

public class ExpenseCreatedMessage : BaseEventMessage
{
	public Guid Id { get; set; }
	public decimal Amount { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
}
