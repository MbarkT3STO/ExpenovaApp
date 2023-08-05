
namespace ExpenseService.Domain.Models;

public class SubscriptionExpense: AuditableEntity<Guid>
{
	public decimal Amount { get; set; }
	public string Description { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public RecurrenceInterval RecurrenceInterval { get; set; }
	public decimal BillingAmount { get; set; }
	public string CategoryId { get; set; }
	public string UserId { get; set; }
	
	
	protected SubscriptionExpense() { }
	
	public SubscriptionExpense(decimal amount, string description, DateTime startDate, DateTime endDate, RecurrenceInterval recurrenceInterval, decimal billingAmount, string categoryId, string userId)
	{
		Amount             = amount;
		Description        = description;
		StartDate          = startDate;
		EndDate            = endDate;
		RecurrenceInterval = recurrenceInterval;
		BillingAmount      = billingAmount;
		CategoryId         = categoryId;
		UserId             = userId;
		
		CreatedDate = DateTime.UtcNow;
		CreatedBy   = userId;
	}
}
