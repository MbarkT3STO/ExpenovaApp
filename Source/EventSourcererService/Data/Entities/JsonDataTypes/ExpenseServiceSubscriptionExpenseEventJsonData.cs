namespace EventSourcererService.Data.Entities.JsonDataTypes;

public class ExpenseServiceSubscriptionExpenseEventJsonData : AuditableJsonEntity<Guid>
{
	public decimal Amount { get; private set; }
	public string Description { get; private set; }
	public DateTime StartDate { get; private set; }
	public DateTime EndDate { get; private set; }
	public RecurrenceInterval RecurrenceInterval { get; private set; }
	public decimal BillingAmount { get; private set; }

	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
	
	public ExpenseServiceSubscriptionExpenseEventJsonData(Guid id, decimal amount, string description, DateTime startDate, DateTime endDate, RecurrenceInterval recurrenceInterval, decimal billingAmount, Guid categoryId, string userId, DateTime createdAt, string createdBy) : base(id, createdAt, createdBy)
	{
		Amount = amount;
		Description = description;
		StartDate = startDate;
		EndDate = endDate;
		RecurrenceInterval = recurrenceInterval;
		BillingAmount = billingAmount;
		CategoryId = categoryId;
		UserId = userId;
	}
}
