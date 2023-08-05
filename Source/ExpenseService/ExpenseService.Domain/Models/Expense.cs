namespace ExpenseService.Domain.Models;

public class Expense: AuditableEntity<Guid>
{
	public decimal Amount { get; set; }
	public DateTime Date { get; set; }
	public string Description { get; set; }
	public string CategoryId { get; set; }
	public string UserId { get; set; }


	protected Expense() { }

	public Expense(decimal amount, DateTime date, string description, string categoryId, string userId)
	{
		Amount      = amount;
		Date        = date;
		Description = description;
		CategoryId  = categoryId;
		UserId      = userId;

		CreatedDate = DateTime.UtcNow;
		CreatedBy   = userId;
	}
}
