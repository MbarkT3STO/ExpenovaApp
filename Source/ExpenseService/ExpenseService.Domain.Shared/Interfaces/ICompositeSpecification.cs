namespace ExpenseService.Domain.Shared.Interfaces;

/// <summary>
/// Represents a composite specification that combines multiple specifications using logical operators.
/// </summary>
/// <typeparam name="T">The type of object that the specification applies to.</typeparam>
public interface ICompositeSpecification<T>: ISpecification<T> where T: class
{
	/// <summary>
	/// Combines the current specification with another specification using the logical AND operator.
	/// </summary>
	/// <param name="other">The other specification to combine with.</param>
	/// <returns>A new composite specification representing the logical AND operation.</returns>
	new ICompositeSpecification<T> And(ISpecification<T> other);

	/// <summary>
	/// Combines the current specification with another specification using the logical OR operator.
	/// </summary>
	/// <param name="other">The other specification to combine with.</param>
	/// <returns>A new composite specification representing the logical OR operation.</returns>
	ICompositeSpecification<T> Or(ISpecification<T> other);

	/// <summary>
	/// Negates the current specification using the logical NOT operator.
	/// </summary>
	/// <param name="other">The specification to negate.</param>
	/// <returns>A new composite specification representing the logical NOT operation.</returns>
	ICompositeSpecification<T> Not(ISpecification<T> other);
}