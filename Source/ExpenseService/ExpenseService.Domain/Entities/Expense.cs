using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Domain.Entities;

public class Expense: AuditableAggregateRoot<Guid>
{
	public decimal Amount { get; private set; }
	public DateTime Date { get; private set; }
	public string Description { get; private set; }
	public Category Category { get; private set; }
	public User User { get; private set; }

	protected Expense() { }

	public Expense(decimal amount, DateTime date, string description, Category category, User user)
	{
		Amount      = amount;
		Date        = date;
		Description = description;
		Category    = category;
		User        = user;

		CreatedAt = DateTime.UtcNow;
		CreatedBy   = user.Id;
	}

	/// <summary>
	/// Validates the expense entity against the given specification and throws a SpecificationException if the specification is not satisfied.
	/// </summary>
	/// <param name="specification">The specification to validate against.</param>
	/// <exception cref="SpecificationException">Thrown if the specification is not satisfied.</exception>
	public void Validate(ISpecification<Expense> specification)
	{
		var satisfactionResult = specification.IsSatisfiedBy(this);

		if (!satisfactionResult.IsSatisfied)
		{
			throw new SpecificationException(satisfactionResult.Errors);
		}
	}


	/// <summary>
	/// Validates the expense entity against the given specification and throws a SpecificationException if the specification is not satisfied.
	/// </summary>
	/// <param name="specification">The specification to validate against.</param>
	/// <exception cref="SpecificationException">Thrown if the specification is not satisfied.</exception>
	public void Validate(ICompositeSpecification<Expense> specification)
	{
		var satisfactionResult = specification.IsSatisfiedBy(this);

		if (!satisfactionResult.IsSatisfied)
		{
			throw new SpecificationException(satisfactionResult.Errors);
		}
	}

	/// <summary>
	/// Sets the ID of the expense.
	/// </summary>
	/// <param name="id">The ID to set.</param>
	public void SetId(Guid id)
	{
		Id = id;
	}
}
