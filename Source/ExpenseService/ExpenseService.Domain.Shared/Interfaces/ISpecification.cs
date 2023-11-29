using System.Linq.Expressions;

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


	/// <summary>
	/// Converts the specification to an expression tree.
	/// </summary>
	/// <returns>An expression tree representing the specification.</returns>
	Expression<Func<T, bool>> ToExpression();
	
	
	/// <summary>
	/// Combines the current specification with another specification using the logical AND operator.
	/// </summary>
	/// <param name="other">The specification to combine with the current specification.</param>
	/// <returns>A new specification that represents the combination of the current specification and the other specification.</returns>
	ISpecification<T> And(ISpecification<T> other);
}
