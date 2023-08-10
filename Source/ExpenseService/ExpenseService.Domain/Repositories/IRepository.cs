using ExpenseService.Domain.Shared.Interfaces;

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
	IQueryable<TEntity> GetAll();
	
	/// <summary>
	/// Gets the entity by id.
	/// </summary>
	TEntity GetById(TKey id);
	
	/// <summary>
	/// Adds the specified entity.
	/// </summary>
	void Add(TEntity entity);
	
	/// <summary>
	/// Updates the specified entity.
	/// </summary>
	void Update(TEntity entity);
	
	/// <summary>
	/// Deletes the specified entity.
	/// </summary>
	void Delete(TEntity entity);
}
