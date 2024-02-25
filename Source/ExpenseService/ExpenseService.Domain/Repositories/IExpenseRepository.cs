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

	/// <summary>
	/// Retrieves the count of expenses for a specific user asynchronously.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The count of expenses.</returns>
	Task<int> GetCountAsync(string userId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves the sum of expenses for a specific user.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The sum of expenses as a decimal.</returns>
	Task<decimal> GetSumAsync(string userId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves the average expense amount for a specific user.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The average expense amount.</returns>
	Task<decimal> GetAverageAsync(string userId, CancellationToken cancellationToken = default);


	/// <summary>
	/// Retrieves the sum of expenses grouped by month and year for a specific user.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task<IEnumerable<(int Month, int Year, decimal Sum)>> GetSumGroupedByMonthAndYearAsync(string userId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves the sum of expenses grouped by year for a specific user.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task<IEnumerable<(int Year, decimal Sum)>> GetSumGroupedByYearAsync(string userId, CancellationToken cancellationToken = default);
}
