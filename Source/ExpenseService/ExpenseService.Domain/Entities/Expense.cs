namespace ExpenseService.Domain.Entities;

public class Expense: AuditableAggregateRoot<Guid>
{
	public decimal Amount { get; private set; }
	public DateTime Date { get; private set; }
	public string Description { get; private set; }
	public Category Category { get; private set; }
	public User User { get; private set; }

	protected Expense() { }

	public Expense(decimal amount, DateTime date, string description, Category category, User user)
	{
		Amount      = amount;
		Date        = date;
		Description = description;
		Category    = category;
		User        = user;
		
		CreatedDate = DateTime.UtcNow;
		CreatedBy   = user.Id;
	}
}
