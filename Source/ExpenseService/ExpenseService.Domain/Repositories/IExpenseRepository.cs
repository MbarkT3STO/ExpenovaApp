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
	Task<IEnumerable<Expense>> GetExpensesByUserAsync(string userId, CancellationToken cancellationToken = default);


	/// <summary>
	/// Retrieves a collection of expenses by category ID asynchronously.
	/// </summary>
	/// <param name="categoryId">The ID of the category.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a collection of expenses.</returns>
	Task<IEnumerable<Expense>> GetExpensesByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);


	/// <summary>
	/// Retrieves expenses by user ID and category ID.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="categoryId">The ID of the category.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the expenses.</returns>
	Task<IEnumerable<Expense>> GetExpensesByUserAndCategoryAsync(string userId, Guid categoryId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Soft deletes an expense.
	/// </summary>
	/// <param name="expense">The expense to be soft deleted.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task SoftDeleteAsync(Expense expense, CancellationToken cancellationToken = default);
}
