namespace Messages.ExpenseServiceMessages.SubscriptionExpense;

/// <summary>
/// Represents a message indicating the deletion of a subscription expense.
/// </summary>
public class SubscriptionExpenseDeletedMessage : BaseEventMessage
{
	public Guid Id { get; set; }
	public string Description { get; set; }
	public decimal Amount { get; set; }
	public string UserId { get; set; }
	public Guid CategoryId { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public byte RecurrenceInterval { get; set; }
	public decimal BillingAmount { get; set; }

	public DateTime CreatedAt { get; set; }
	public string CreatedBy { get; set; }
	public DateTime? LastUpdatedAt { get; set; }
	public string LastUpdatedBy { get; set; }
	public bool IsDeleted { get; set; }
	public DateTime? DeletedAt { get; set; }
	public string? DeletedBy { get; set; }
}
