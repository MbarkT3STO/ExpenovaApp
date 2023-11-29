using System.Linq.Expressions;
using ExpenseService.Domain.Shared.Common;
using ExpenseService.Domain.Shared.Interfaces;

namespace ExpenseService.Domain.Specifications;

/// <summary>
/// Base class for creating specifications.
/// </summary>
/// <typeparam name="T">The type of entity that the specification can be applied to.</typeparam>
public abstract class Specification<T> : ISpecification<T> where T : class
{
	protected abstract string UnSatisfiedSpecificationErrorMessage { get; }
	
	
	/// <inheritdoc cref="ISpecification{T}.ToExpression"/>
	public abstract Expression<Func<T, bool>> ToExpression();


	/// <inheritdoc cref="ISpecification{T}.IsSatisfiedBy"/>
	public virtual bool IsSatisfiedBy(T entity)
	{
		var predicate = ToExpression().Compile();
		var result    = predicate(entity);

		return result;
	}
	
	
	/// <inheritdoc cref="ISpecification{T}.And"/>
	public virtual ISpecification<T> And(ISpecification<T> other)
	{
		return new AndSpecification<T>(this, other);
	}
	
	
	/// <inheritdoc cref="ISpecification{T}.GetErrors"/>
	public virtual Error GetError()
	{
		var error = new Error(UnSatisfiedSpecificationErrorMessage);

		return error;
	}
}