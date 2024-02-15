using ExpenseService.Domain.Events;

namespace ExpenseService.Domain.Repositories;

/// <summary>
/// Represents a generic repository interface
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the key of the entity.</typeparam>
public interface IRepository<TEntity, TKey> : IDisposable where TEntity : class
{
	/// <summary>
	/// Gets all entities.
	/// </summary>
	IQueryable<TEntity> Get();

	/// <summary>
	/// Asynchronously gets all entities.
	/// </summary>
	Task<IEnumerable<TEntity>> GetAsync(CancellationToken cancellationToken = default);

	/// <summary>
	/// Gets the entity by id.
	/// </summary>
	TEntity GetById(TKey id);

	/// <summary>
	/// Retrieves an entity by its unique identifier asynchronously.
	/// </summary>
	/// <param name="id">The ID of the entity to retrieve.</param>
	Task<TEntity> GetByIdAsync(TKey id);

	/// <summary>
	/// Retrieves an entity by its unique identifier asynchronously.
	/// </summary>
	/// <param name="id">The unique identifier of the entity.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the entity with the specified identifier.</returns>
	Task<TEntity> GetByIdAsync(TKey id, CancellationToken cancellationToken);


	/// <summary>
	/// Retrieves an entity by its ID or throws an exception if not found.
	/// </summary>
	/// <param name="id">The ID of the entity to retrieve.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the entity.</returns>
	Task<TEntity> GetByIdOrThrowAsync(TKey id, CancellationToken cancellationToken = default);


	/// <summary>
	/// Asynchronously adds a new entity.
	/// </summary>
	/// <param name="entity">The entity to add.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
	Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);


	/// <summary>
	/// Updates the specified entity.
	/// </summary>
	void Update(TEntity entity);

	/// <summary>
	/// Asynchronously updates the specified entity.
	/// </summary>
	/// <param name="entity">The entity to update.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

	/// <summary>
	/// Deletes the specified entity.
	/// </summary>
	void Delete(TEntity entity);

	/// <summary>
	/// Asynchronously deletes the specified entity.
	/// </summary>
	Task DeleteAsync(TEntity entity);

	/// <summary>
	/// Asynchronously deletes the specified entity.
	/// </summary>
	/// <param name="entity">The entity to delete.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A task that represents the asynchronous operation.</returns>
	Task DeleteAsync(TEntity entity, CancellationToken cancellationToken);



	/// <summary>
	/// Checks if an entity exists based on the provided ID.
	/// </summary>
	/// <returns>A task representing the asynchronous operation.</returns>
	/// <exception cref="Exception">Thrown if the entity does not exist.</exception>
	Task ThrowIfNotExistAsync(TKey id, CancellationToken cancellationToken = default);
}
