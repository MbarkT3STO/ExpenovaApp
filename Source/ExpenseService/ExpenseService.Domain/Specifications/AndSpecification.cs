using System.Linq.Expressions;
using ExpenseService.Domain.Shared.Common;
using ExpenseService.Domain.Shared.Interfaces;

namespace ExpenseService.Domain.Specifications;

/// <summary>
/// Represents a specification that combines two specifications using a logical AND operator.
/// </summary>
/// <typeparam name="T">The type of entity that the specification applies to.</typeparam>
public class AndSpecification<T>: CompositeSpecification<T> where T: class
{
	private readonly ISpecification<T> _left;
	private readonly ISpecification<T> _right;


	public AndSpecification( ISpecification<T> left, ISpecification<T> right )
	{
		_left  = left;
		_right = right;
	}
	

	public  Expression<Func<T, bool>> ToExpression()
	{
		var leftExpression  = _left.ToExpression();
		var rightExpression = _right.ToExpression();
		
		var andExpression = Expression.AndAlso( leftExpression.Body, rightExpression.Body );
		
		return Expression.Lambda<Func<T, bool>>( andExpression, leftExpression.Parameters.Single() );
	}
	
	
}
