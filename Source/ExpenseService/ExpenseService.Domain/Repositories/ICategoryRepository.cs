using ExpenseService.Domain.Events;

namespace ExpenseService.Domain.Repositories;

public interface ICategoryRepository : IRepository<Category, Guid>
{
	/// <summary>
	/// Represents an asynchronous operation that can return a result.
	/// </summary>
	/// <typeparam name="TResult">The type of the result produced by the task.</typeparam>
	void Add(Category entity, CategoryCreatedEvent categoryCreatedEvent);

	/// <summary>
	/// Adds an entity asynchronously to the repository.
	/// </summary>
	/// <param name="entity">The entity to add.</param>
	/// <param name="categoryCreatedEvent">The category created event.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	Task AddAsync(Category entity, CategoryCreatedEvent categoryCreatedEvent);



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
	Task<Category> GetByNameAndUserIdAsync(string name, string userId);

	/// <summary>
	/// Retrieves a category by its ID and user ID asynchronously.
	/// </summary>
	/// <param name="id">The ID of the category.</param>
	/// <param name="userId">The ID of the user.</param>
	/// <returns>The category with the specified ID and user ID.</returns>
	Task<Category> GetByIdAndUserIdAsync(Guid id, string userId);
}
