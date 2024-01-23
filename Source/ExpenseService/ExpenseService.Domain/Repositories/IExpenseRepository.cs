namespace ExpenseService.Domain.Repositories;

public interface IExpenseRepository : IRepository<Expense, Guid>
{
	Task<IQueryable<Expense>> GetExpensesByUserIdAsync(string userId);
	Task<IQueryable<Expense>> GetExpensesByUserIdAndCategoryIdAsync(string userId, Guid categoryId);
}
