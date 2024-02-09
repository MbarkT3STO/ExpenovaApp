namespace ExpenseService.Domain.Repositories;

public interface ISubscriptionExpenseRepository : IRepository<SubscriptionExpense, Guid>
{
	Task<IQueryable<SubscriptionExpense>> GetSubscriptionExpensesByUserIdAsync(int userId);
	Task<IQueryable<SubscriptionExpense>> GetSubscriptionExpensesByUserIdAndCategoryIdAsync(int userId, int categoryId);
}
