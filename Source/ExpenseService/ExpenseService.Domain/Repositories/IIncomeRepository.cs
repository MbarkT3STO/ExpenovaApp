using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseService.Domain.Repositories;

/// <summary>
/// Represents a repository for managing income entities.
/// </summary>
public interface IIncomeRepository : IRepository<Income, Guid>
{
	/// <summary>
	/// Retrieves a collection of incomes for a specific user.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a collection of incomes.</returns>
	Task<IEnumerable<Income>> GetIncomesByUserAsync(string userId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves a collection of incomes for a specific user and category.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="categoryId">The ID of the category.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a collection of incomes.</returns>
	Task<IEnumerable<Income>> GetIncomesByUserAndCategoryAsync(string userId, Guid categoryId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves the count of incomes for a specific user asynchronously.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The count of incomes.</returns>
	Task<int> GetCountAsync(string userId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves the sum of incomes for a specific user asynchronously.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The sum of incomes.</returns>
	Task<decimal> GetSumAsync(string userId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves the sum of income grouped by month and year for a specific user asynchronously.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a collection of tuples, where each tuple contains the month, year, and sum of income.</returns>
	Task<IEnumerable<(int Month, int Year, decimal Sum)>> GetSumGroupedByMonthAndYearAsync(string userId, CancellationToken cancellationToken = default);


	/// <summary>
	/// Retrieves the sum of incomes grouped by year for a specific user.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a collection of tuples, where each tuple represents a year and its corresponding sum of incomes.</returns>
	Task<IEnumerable<(int Year, decimal Sum)>> GetSumGroupedByYearAsync(string userId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves the sum of expenses grouped by category for a specific user asynchronously.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains a collection of tuples, where each tuple contains the category name and the sum of expenses for that category.</returns>
	Task<IEnumerable<(string Category, decimal Sum)>> GetSumGroupedByCategoryAsync(string userId, CancellationToken cancellationToken = default);

	/// <summary>
	/// Retrieves the top category and its sum of income for a specific user.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A tuple containing the top category and its sum of income, or null if no income is found.</returns>
	Task<(string Category, decimal Sum)?> GetTopCategoryAsync(string userId, CancellationToken cancellationToken = default);
}
