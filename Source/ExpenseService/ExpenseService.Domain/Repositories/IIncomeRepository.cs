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
}
