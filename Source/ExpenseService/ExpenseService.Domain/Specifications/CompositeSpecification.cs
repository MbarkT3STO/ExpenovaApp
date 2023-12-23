using System.Linq.Expressions;
using ExpenseService.Domain.Shared.Common;
using ExpenseService.Domain.Shared.Interfaces;

namespace ExpenseService.Domain.Specifications;

/// <summary>
/// Composite specification that aggregates multiple specifications.
/// </summary>
/// <typeparam name="T">The type of entity that the specification can be applied to.</typeparam>
public abstract class CompositeSpecification<T>: ICompositeSpecification<T> where T: class
{
	/// <summary>
	/// Indicates whether the specifications should be configured from the base class.
	/// <br/>
	/// This means that the base class will call <see cref="ConfigureSpecifications"/> to configure the specifications.
	/// </summary>
	protected bool ShouldConfigureSpecificationsFromTheBase { get; set; } = true;
	public    List<ISpecification<T>> Specifications { get; }             = new();

	/// <summary>
	/// Creates a new instance of <see cref="CompositeSpecification{T}"/>.
	/// </summary>
	/// <param name="shouldConfigureSpecificationsFromTheBase">Indicates whether the specifications should be configured from the base class's constructor.</param>
	protected CompositeSpecification(bool shouldConfigureSpecificationsFromTheBase = true)
	{
		ShouldConfigureSpecificationsFromTheBase = shouldConfigureSpecificationsFromTheBase;
		if (ShouldConfigureSpecificationsFromTheBase)
		{
			ConfigureSpecifications();
		}
	}

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

	public virtual void ConfigureSpecifications()
	{
		throw new NotImplementedException();
	}
}
