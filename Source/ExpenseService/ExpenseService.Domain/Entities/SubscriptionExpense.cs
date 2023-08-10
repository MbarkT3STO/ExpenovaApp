
namespace ExpenseService.Domain.Entities;

public class SubscriptionExpense: AuditableAggregateRoot<Guid>
{
	public decimal Amount { get; private set; }
	public string Description { get; private set; }
	public DateTime StartDate { get; private set; }
	public DateTime EndDate { get; private set; }
	public RecurrenceInterval RecurrenceInterval { get; private set; }
	public decimal BillingAmount { get; private set; }
	public Category Category { get; private set; }
	public User User { get; private set; }
	
	
	protected SubscriptionExpense() { }
	
	public SubscriptionExpense(decimal amount, string description, DateTime startDate, DateTime endDate, RecurrenceInterval recurrenceInterval, decimal billingAmount, Category category, User user)
	{
		Amount             = amount;
		Description        = description;
		StartDate          = startDate;
		EndDate            = endDate;
		RecurrenceInterval = recurrenceInterval;
		BillingAmount      = billingAmount;
		Category           = category;
		User               = user;
		
		CreatedDate = DateTime.UtcNow;
		CreatedBy   = user.Id;
	}
}
