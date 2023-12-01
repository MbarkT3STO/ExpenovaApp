using System.Linq.Expressions;
using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Domain.Shared.Interfaces;

/// <summary>
/// Represents a specification that defines a condition that an entity must satisfy.
/// </summary>
/// <typeparam name="T">The type of entity.</typeparam>
public interface ISpecification<T> where T: class
{
	/// <summary>
	/// Determines if the specification is satisfied by the specified entity.
	/// </summary>
	/// <typeparam name="T">The type of entity being evaluated.</typeparam>
	SatisfactionResult IsSatisfiedBy(T entity);


	/// <summary>
	/// Converts the specification to an expression tree.
	/// </summary>
	/// <returns>An expression tree representing the specification.</returns>
	Expression<Func<T, bool>> ToExpression();	
}
