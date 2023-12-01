using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Domain.Shared.Interfaces;

/// <summary>
/// Represents a composite specification that combines multiple specifications using logical operators.
/// </summary>
/// <typeparam name="T">The type of object that the specification applies to.</typeparam>
public interface ICompositeSpecification<T> where T : class
{
	List<ISpecification<T>> Specifications { get; }
	void AddSpecification(ISpecification<T> specification);
    new SatisfactionResult IsSatisfiedBy(T entity);
	void ConfigureSpecifications();
}