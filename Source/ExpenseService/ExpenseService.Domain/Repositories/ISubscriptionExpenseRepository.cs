namespace ExpenseService.Domain.Repositories;

/// <summary>
/// Represents a repository for managing subscription expenses.
/// </summary>
public interface ISubscriptionExpenseRepository : IRepository<SubscriptionExpense, Guid>
{
	/// <summary>
	/// Retrieves the subscription expenses associated with a specific user asynchronously.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the collection of subscription expenses.</returns>
	Task<IEnumerable<SubscriptionExpense>> GetSubscriptionExpensesByUserAsync(string userId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves subscription expenses by category asynchronously.
	/// </summary>
	/// <param name="categoryId">The ID of the category.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a queryable collection of subscription expenses.</returns>
	Task<IEnumerable<SubscriptionExpense>> GetSubscriptionExpensesByCategoryAsync(Guid categoryId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a collection of subscription expenses based on the specified user ID and category ID.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="categoryId">The ID of the category.</param>
	/// <param name="cancellationToken">The token to monitor for cancellation requests.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a queryable collection of subscription expenses.</returns>
	Task<IEnumerable<SubscriptionExpense>> GetSubscriptionExpensesByUserAndCategoryAsync(string userId, Guid categoryId, CancellationToken cancellationToken = default);


	/// <summary>
	/// Retrieves the count of subscription expenses for a specific user.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The count of subscription expenses.</returns>
	Task<int> GetCountAsync(string userId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves the sum of expenses for a specific user.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The sum of expenses for the user.</returns>
	Task<decimal> GetSumAsync(string userId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves the average expense amount for a specific user.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The average expense amount.</returns>
	Task<decimal> GetAverageAsync(string userId, CancellationToken cancellationToken = default);


	/// <summary>
	/// Retrieves the sum of expenses grouped by year for a specific user.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task<IEnumerable<(int Year, decimal Sum)>> GetSumGroupedByYearAsync(string userId, CancellationToken cancellationToken = default);

}
