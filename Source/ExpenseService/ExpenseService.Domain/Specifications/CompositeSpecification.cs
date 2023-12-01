using System.Linq.Expressions;
using ExpenseService.Domain.Shared.Common;
using ExpenseService.Domain.Shared.Interfaces;

namespace ExpenseService.Domain.Specifications;

/// <summary>
/// Composite specification that aggregates multiple specifications.
/// </summary>
/// <typeparam name="T">The type of entity that the specification can be applied to.</typeparam>
public class CompositeSpecification<T>: ICompositeSpecification<T> where T: class
{
    public List<ISpecification<T>> Specifications { get;}

    /// <summary>
    /// Adds a specification to the list of aggregated specifications.
    /// </summary>
    /// <param name="specification">The specification to add.</param>
    public void AddSpecification(ISpecification<T> specification)
	{
		Specifications.Add(specification);
	}

	public Expression<Func<T, bool>> ToExpression()
	{
		if (!Specifications.Any())
		{
			// If there are no specifications, return a constant expression that is always true.
			return _ => true;
		}

		// Combine expressions using AND operator
		var parameter          = Expression.Parameter(typeof(T), "entity");
		var combinedExpression = Specifications.Select(spec => spec.ToExpression().Body)
												.Aggregate((expr1, expr2) => Expression.AndAlso(expr1, expr2));

		return Expression.Lambda<Func<T, bool>>(combinedExpression, parameter);
	}

	public SatisfactionResult IsSatisfiedBy(T entity)
	{
		var errors = new List<Error>();

		foreach (var specification in Specifications)
		{
			var satisfactionResult = specification.IsSatisfiedBy(entity);
			if (!satisfactionResult.IsSatisfied)
			{
				errors.AddRange(satisfactionResult.Errors);
			}
		}

		if (errors.Any())
		{
			// If there are errors, return a SatisfactionResult indicating that the specifications are not satisfied
			return SatisfactionResult.NotSatisfied(errors);
		}

		// If all specifications are satisfied, return a SatisfactionResult indicating success
		return SatisfactionResult.Satisfied();
	}

    public void ConfigureSpecifications()
    {
        throw new NotImplementedException();
    }
}
