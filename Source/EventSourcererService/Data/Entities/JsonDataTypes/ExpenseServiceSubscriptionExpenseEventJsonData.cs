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
}
