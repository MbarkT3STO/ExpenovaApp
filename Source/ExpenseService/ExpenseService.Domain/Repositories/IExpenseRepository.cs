namespace ExpenseService.Domain.Repositories;

/// <summary>
/// Represents a repository for managing expenses.
/// </summary>
public interface IExpenseRepository : IRepository<Expense, Guid>
{
	/// <summary>
	/// Retrieves expenses by user ID.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the expenses.</returns>
	Task<IQueryable<Expense>> GetExpensesByUserIdAsync(string userId);

	/// <summary>
	/// Retrieves expenses by user ID and category ID.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="categoryId">The ID of the category.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the expenses.</returns>
	Task<IQueryable<Expense>> GetExpensesByUserIdAndCategoryIdAsync(string userId, Guid categoryId);

	/// <summary>
	/// Soft deletes an expense.
	/// </summary>
	/// <param name="expense">The expense to be soft deleted.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task SoftDeleteAsync(Expense expense, CancellationToken cancellationToken = default);
}
