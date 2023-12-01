using System.Linq.Expressions;
using ExpenseService.Domain.Shared.Common;
using ExpenseService.Domain.Shared.Interfaces;

namespace ExpenseService.Domain.Specifications;

/// <summary>
/// Represents a composite specification that performs a logical OR operation between two specifications.
/// </summary>
/// <typeparam name="T">The type of entity that the specification is applied to.</typeparam>
public class OrSpecification<T> : CompositeSpecification<T> where T : class
{
	private readonly ISpecification<T> _left;
	private readonly ISpecification<T> _right;

	/// <summary>
	/// Initializes a new instance of the <see cref="OrSpecification{T}"/> class with the specified left and right specifications.
	/// </summary>
	/// <param name="left">The left specification.</param>
	/// <param name="right">The right specification.</param>
	/// <exception cref="ArgumentNullException">Thrown when either the left or right specification is null.</exception>
	public OrSpecification(ISpecification<T> left, ISpecification<T> right)
	{
		_left = left ?? throw new ArgumentNullException(nameof(left));
		_right = right ?? throw new ArgumentNullException(nameof(right));
	}

	/// <inheritdoc/>
	public Expression<Func<T, bool>> ToExpression()
	{
		var leftExpression = _left.ToExpression();
		var rightExpression = _right.ToExpression();

		var orExpression = Expression.OrElse(leftExpression.Body, rightExpression.Body);

		return Expression.Lambda<Func<T, bool>>(orExpression, leftExpression.Parameters.Single());
	}


}
