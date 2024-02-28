namespace EventSourcererService.Data.Entities.JsonDataTypes;

public class ExpenseServiceSubscriptionExpenseEventJsonData: AuditableJsonEntity<Guid>
{
	public string Description { get; set; }
	public decimal Amount { get; set; }
	public string UserId { get; set; }
	public Guid CategoryId { get; set; }
	public DateTime StartDate { get; set; }
	public DateTime EndDate { get; set; }
	public byte RecurrenceInterval { get; set; }
	public decimal BillingAmount { get; set; }


	public ExpenseServiceSubscriptionExpenseEventJsonData(Guid id, decimal amount, string description, string userId, Guid categoryId, DateTime startDate, DateTime endDate, byte recurrenceInterval, decimal billingAmount, DateTime createdAt, string createdBy, DateTime? lastUpdatedAt, string lastUpdatedBy, DateTime? deletedAt, string? deletedBy): base(id, createdAt, createdBy, lastUpdatedAt, lastUpdatedBy, deletedAt, deletedBy)
	{
		Description        = description;
		Amount             = amount;
		UserId             = userId;
		CategoryId         = categoryId;
		StartDate          = startDate;
		EndDate            = endDate;
		RecurrenceInterval = recurrenceInterval;
		BillingAmount      = billingAmount;
	}
}