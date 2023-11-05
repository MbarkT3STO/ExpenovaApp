namespace ExpenseService.Infrastructure.Data.Entities;

/// <summary>
/// Represents a category entity in the database.
/// </summary>
public class CategoryEntity : AuditableEntity<Guid>
{
	public string Name { get; set; }
	public string Description { get; set; }
	public string UserId { get; set; }
	public UserEntity User { get; set; }
	
	public virtual ICollection<ExpenseEntity> Expenses { get; set; }
	public virtual ICollection<SubscriptionExpenseEntity> SubscriptionExpenses { get; set; }
}
