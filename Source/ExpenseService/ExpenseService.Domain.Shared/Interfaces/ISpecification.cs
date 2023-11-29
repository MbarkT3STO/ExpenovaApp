namespace ExpenseService.Domain.Shared.Interfaces;

/// <summary>
/// Represents a specification that defines a condition that an entity must satisfy.
/// </summary>
/// <typeparam name="T">The type of entity.</typeparam>
public interface ISpecification<T> where T: class
{
	/// <summary>
	/// Determines whether the specified entity satisfies the specification.
	/// </summary>
	/// <param name="entity">The entity to be checked.</param>
	/// <returns><c>true</c> if the entity satisfies the specification; otherwise, <c>false</c>.</returns>
	bool IsSatisfiedBy(T entity);
}
