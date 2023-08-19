namespace EventSourcererService.Data.Entities.JsonDataTypes;

public class ExpenseServiceExpenseEventJsonData : AuditableJsonEntity<Guid>
{
	public decimal Amount { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
}
