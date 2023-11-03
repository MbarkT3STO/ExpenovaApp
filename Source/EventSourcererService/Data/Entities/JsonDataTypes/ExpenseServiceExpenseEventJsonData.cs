namespace EventSourcererService.Data.Entities.JsonDataTypes;

public class ExpenseServiceExpenseEventJsonData: AuditableJsonEntity<Guid>
{
	public decimal Amount { get; set; }
	public string Description { get; set; }
	public DateTime Date { get; set; }
	public Guid CategoryId { get; set; }
	public string UserId { get; set; }
	
	public ExpenseServiceExpenseEventJsonData(Guid id, decimal amount, string description, DateTime date, Guid categoryId, string userId, DateTime createdAt, string createdBy): base(id, createdAt, createdBy)
	{
		Amount      = amount;
		Description = description;
		Date        = date;
		CategoryId  = categoryId;
		UserId      = userId;
	}
	
	public ExpenseServiceExpenseEventJsonData(Guid id, decimal amount, string description, DateTime date, Guid categoryId, string userId, DateTime createdAt, string createdBy, DateTime? updatedAt, string updatedBy): base(id, createdAt, createdBy, updatedAt, updatedBy)
	{
		Amount      = amount;
		Description = description;
		Date        = date;
		CategoryId  = categoryId;
		UserId      = userId;
	}
	
	public ExpenseServiceExpenseEventJsonData(Guid id, decimal amount, string description, DateTime date, Guid categoryId, string userId, DateTime createdAt, string createdBy, DateTime? updatedAt, string updatedBy, DateTime? deletedAt, string deletedBy): base(id, createdAt, createdBy, updatedAt, updatedBy, deletedAt, deletedBy)
	{
		Amount      = amount;
		Description = description;
		Date        = date;
		CategoryId  = categoryId;
		UserId      = userId;
	}
}
