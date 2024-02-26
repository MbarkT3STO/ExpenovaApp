using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Domain.Entities;

public class Income: AuditableAggregateRoot<Guid>
{
	public string Description { get; private set; }
	public DateTime Date { get; private set; }
	public decimal Amount { get; private set; }
	public Category Category { get; private set; }
	public User User { get; private set; }

	protected Income() { }

	public Income(string description, DateTime date, decimal amount, Category category, User user)
	{
		Description = description;
		Date        = date;
		Amount      = amount;
		Category    = category;
		User        = user;

		CreatedAt = DateTime.UtcNow;
		CreatedBy = user.Id;
	}

	/// <summary>
	/// Validates the income entity against the given specification and throws a SpecificationException if the specification is not satisfied.
	/// </summary>
	/// <param name="specification">The specification to validate against.</param>
	/// <exception cref="SpecificationException">Thrown if the specification is not satisfied.</exception>
	public void Validate(ISpecification<Income> specification)
	{
		var satisfactionResult = specification.IsSatisfiedBy(this);

		if (!satisfactionResult.IsSatisfied)
		{
			throw new SpecificationException(satisfactionResult.Errors);
		}
	}


	/// <summary>
	/// Validates the income entity against the specified specification and throws a SpecificationException if the specification is not satisfied.
	/// </summary>
	/// <param name="specification">The specification to validate against.</param>
	/// <exception cref="SpecificationException">Thrown if the specification is not satisfied.</exception>
	public void Validate(ICompositeSpecification<Income> specification)
	{
		var satisfactionResult = specification.IsSatisfiedBy(this);

		if (!satisfactionResult.IsSatisfied)
		{
			throw new SpecificationException(satisfactionResult.Errors);
		}
	}


	/// <summary>
	/// Sets the ID of the income.
	/// </summary>
	/// <param name="id">The ID to set.</param>
	public void SetId(Guid id)
	{
		Id = id;
	}
}
