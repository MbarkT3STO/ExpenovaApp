namespace ExpenseService.Infrastructure.Data.Entities;

public class CategoryEntity
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	
	public string UserId { get; set; }
	public UserEntity User { get; set; }
	
	public virtual ICollection<ExpenseEntity> Expenses { get; set; }
	public virtual ICollection<SubscriptionExpenseEntity> SubscriptionExpenses { get; set; }
}
