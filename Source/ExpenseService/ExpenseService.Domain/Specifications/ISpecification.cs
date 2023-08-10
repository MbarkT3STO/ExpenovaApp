using System.Linq.Expressions;

namespace ExpenseService.Domain.Specifications;

public interface ISpecification<T>
{
	/// <summary>
	/// Determines whether the specified entity satisfies the specification.
	/// </summary>
	/// <param name="entity">The entity to check.</param>
	/// <returns>True if the entity satisfies the specification, otherwise false.</returns>
	bool IsSatisfiedBy(T entity);
	
	/// <summary>
	/// Converts the specification to an expression tree.
	/// </summary>
	/// <returns>An expression tree representing the specification.</returns>
	Expression<Func<T, bool>> ToExpression();
}
