namespace ExpenseService.Domain.Repositories;

public interface ISubscriptionExpenseRepository : IRepository<SubscriptionExpense, int>
{
	Task<IQueryable<SubscriptionExpense>> GetSubscriptionExpensesByUserIdAsync(int userId);
	Task<IQueryable<SubscriptionExpense>> GetSubscriptionExpensesByUserIdAndCategoryIdAsync(int userId, int categoryId);
}
