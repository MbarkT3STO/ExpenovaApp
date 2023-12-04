namespace ExpenseService.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category, Guid>
{
	/// <summary>
	/// Retrieves a collection of categories associated with the specified user ID.
	/// </summary>
	/// <param name="userId">The ID of the user whose categories should be retrieved.</param>
	/// <returns>A collection of categories associated with the specified user ID.</returns>
	Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(string userId);
	
	/// <summary>
	/// Retrieves a collection of categories associated with the specified user ID.
	/// </summary>
	/// <param name="userId">The ID of the user whose categories should be retrieved.</param>
	/// <param name="cancellationToken">A cancellation token.</param>	
	/// <returns>A collection of categories associated with the specified user ID.</returns>
	Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(string userId, CancellationToken cancellationToken);
	
	/// <summary>
	/// Retrieves a category by its name and user ID asynchronously.
	/// </summary>
	/// <param name="name">The name of the category.</param>
	/// <param name="userId">The ID of the user.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the category.</returns>
	Task<Category> GetByNameAsync(string name, string userId);
}
