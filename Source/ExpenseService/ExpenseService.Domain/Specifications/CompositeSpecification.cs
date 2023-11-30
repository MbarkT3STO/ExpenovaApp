using System.Linq.Expressions;
using ExpenseService.Domain.Shared.Common;
using ExpenseService.Domain.Shared.Interfaces;

namespace ExpenseService.Domain.Specifications;

public class CompositeSpecification<T>: ICompositeSpecification<T> where T: class
{
	protected virtual string UnSatisfiedSpecificationErrorMessage { get; }
	public readonly Expression<Func<T, bool>> CompositeSpecificationExpression;
	private IList<ISpecification<T>> _innerSpecifications;

	private CompositeSpecification(Expression<Func<T, bool>> expression)
	{
		CompositeSpecificationExpression = expression;
	}

	public ICompositeSpecification<T> And(ISpecification<T> other)
	{
		var leftExpression  = this.ToExpression();
		var rightExpression = other.ToExpression();

		var andExpression = Expression.AndAlso(leftExpression.Body, rightExpression.Body);

		var compositeSpecificationExpression = Expression.Lambda<Func<T, bool>>(andExpression, leftExpression.Parameters.Single());
		var compositeSpecification           = new CompositeSpecification<T>(compositeSpecificationExpression);

		return compositeSpecification;
	}

	public ICompositeSpecification<T> Or(ISpecification<T> other)
	{
		throw new NotImplementedException();
	}

	public ICompositeSpecification<T> Not(ISpecification<T> other)
	{
		throw new NotImplementedException();
	}

	public bool IsSatisfiedBy(T entity)
	{
		throw new NotImplementedException();
	}

	public Expression<Func<T, bool>> ToExpression()
	{
		throw new NotImplementedException();
	}

	ISpecification<T> ISpecification<T>.And(ISpecification<T> other)
	{
		throw new NotImplementedException();
	}

	public Error GetError()
	{
		throw new NotImplementedException();
	}
}