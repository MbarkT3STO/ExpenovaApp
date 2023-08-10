namespace ExpenseService.Domain.Repositories;

public interface IExpenseRepository : IRepository<Expense, int>
{
	Task<IQueryable<Expense>> GetExpensesByUserIdAsync(int userId);
	Task<IQueryable<Expense>> GetExpensesByUserIdAndCategoryIdAsync(int userId, int categoryId);
}
