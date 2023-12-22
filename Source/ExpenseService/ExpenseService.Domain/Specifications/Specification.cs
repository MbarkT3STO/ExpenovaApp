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
	private readonly List<(Expression<Func<T, bool>> Condition, string ErrorMessage)> conditions;

	protected Specification()
	{
		conditions = new List<(Expression<Func<T, bool>>, string)>();
		ConfigureConditions();
	}

	/// <summary>
	/// Adds a condition to the specification with a corresponding error message.
	/// </summary>
	/// <param name="condition">The condition to add.</param>
	/// <param name="errorMessage">The error message for the condition when false.</param>
	protected void AddCondition(Expression<Func<T, bool>> condition, string errorMessage)
	{
		conditions.Add((condition, errorMessage));
	}



	/// <inheritdoc cref="ISpecification{T}.IsSatisfiedBy"/>
	public virtual SatisfactionResult IsSatisfiedBy(T entity)
	{
		foreach (var (condition, errorMessage) in conditions)
		{
			var predicate = condition.Compile();
			if (!predicate(entity))
			{
				var error = new Error(errorMessage);
				return SatisfactionResult.NotSatisfied(error);
			}
		}

		return SatisfactionResult.Satisfied();
	}



	/// <inheritdoc cref="ISpecification{T}.ToExpression"/>
	public virtual Expression<Func<T, bool>> ToExpression()
	{
		if (!conditions.Any())
		{
			throw new InvalidOperationException("Specification must have at least one condition.");
		}

		var combinedExpression = conditions
			.Select(condition => condition.Condition.Body)
			.Aggregate(Expression.AndAlso);

		return Expression.Lambda<Func<T, bool>>(combinedExpression, conditions.First().Condition.Parameters);
	}


	/// <summary>
	/// Configures the conditions for the specification.
	/// </summary>
	protected abstract void ConfigureConditions();


	public List<(Expression<Func<T, bool>> Condition, string ErrorMessage)> GetConditions()
	{
		return conditions;
	}

}

