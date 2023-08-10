namespace ExpenseService.Infrastructure.Data.Entities;

public class UserEntity
{
	public string Id { get; private set; }
	public string FirstName { get; private set; }
	public string LastName { get; private set; }
	public DateTime CreatedAt { get; private set; }
	public DateTime? UpdatedAt { get; private set; }

	public virtual ICollection<CategoryEntity> Categories { get; set; }
	public virtual ICollection<ExpenseEntity> Expenses { get; set; }
	public virtual ICollection<SubscriptionExpenseEntity> SubscriptionExpenses { get; set; }
	
}
