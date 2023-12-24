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
	private readonly List<(Expression<Func<T, bool>> Condition, string ErrorMessage)> rules;

	protected Specification()
	{
		rules = new List<(Expression<Func<T, bool>>, string)>();
		ConfigureRules();
	}


	/// <summary>
	/// Adds a rule to the specification.
	/// </summary>
	/// <param name="condition">The condition/rule that must be satisfied.</param>
	/// <param name="errorMessage">The error message to be displayed if the condition/rule is not satisfied.</param>
	protected void AddRule(Expression<Func<T, bool>> condition, string errorMessage)
	{
		rules.Add((condition, errorMessage));
	}



	/// <inheritdoc cref="ISpecification{T}.IsSatisfiedBy"/>
	public virtual SatisfactionResult IsSatisfiedBy(T entity)
	{
		foreach (var (rule, errorMessage) in rules)
		{
			var predicate = rule.Compile();
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
		if (!rules.Any())
		{
			throw new InvalidOperationException("Specification must have at least one condition/rule.");
		}

		var combinedExpression = rules
			.Select(rule => rule.Condition.Body)
			.Aggregate(Expression.AndAlso);

		return Expression.Lambda<Func<T, bool>>(combinedExpression, rules.First().Condition.Parameters);
	}


	/// <summary>
	/// Configures the conditions/rules for the specification.
	/// </summary>
	protected abstract void ConfigureRules();


	public List<(Expression<Func<T, bool>> Condition, string ErrorMessage)> GetRules()
	{
		return rules;
	}

}

