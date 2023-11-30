using System.Linq.Expressions;
using ExpenseService.Domain.Shared.Common;
using ExpenseService.Domain.Shared.Interfaces;

namespace ExpenseService.Domain.Specifications;

public class CompositeSpecification<T>: ICompositeSpecification<T> where T: class
{
	private protected readonly string _unSatisfiedSpecificationErrorMessage;
	
	protected CompositeSpecification()
	{
		
	}


	public ICompositeSpecification<T> And(ISpecification<T> other)
	{
		return new AndSpecification<T>(this, other);
	}


	ISpecification<T> ISpecification<T>.And(ISpecification<T> other)
	{
		return And(other);
	}


	public ICompositeSpecification<T> Or(ISpecification<T> other)
	{
		return new OrSpecification<T>(this, other);
	}

	public virtual bool IsSatisfiedBy(T entity)
	{
		var predicate = ToExpression().Compile();
		var result    = predicate(entity);

		return result;
	}

	public virtual Expression<Func<T, bool>> ToExpression()
	{
		throw new NotImplementedException();	
	}

	public virtual Error GetError()
	{
		var error = new Error(_unSatisfiedSpecificationErrorMessage);

		return error;
	}
}