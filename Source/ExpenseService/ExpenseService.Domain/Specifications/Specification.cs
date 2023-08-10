using System.Linq.Expressions;

namespace ExpenseService.Domain.Specifications;

/// <summary>
/// Base class for creating specifications.
/// </summary>
/// <typeparam name="T">The type of entity that the specification can be applied to.</typeparam>
public abstract class Specification<T> : ISpecification<T> where T : class
{
	/// <inheritdoc cref="ISpecification{T}.ToExpression"/>
	public abstract Expression<Func<T, bool>> ToExpression();


	/// <inheritdoc cref="ISpecification{T}.IsSatisfiedBy"/>
	public bool IsSatisfiedBy(T entity)
	{
		var predicate = ToExpression().Compile();
		return predicate(entity);
	}
}
