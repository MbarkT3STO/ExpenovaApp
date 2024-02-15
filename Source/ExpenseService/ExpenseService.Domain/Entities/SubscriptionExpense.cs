
using ExpenseService.Domain.Shared.Common;

namespace ExpenseService.Domain.Entities;

public class SubscriptionExpense: AuditableAggregateRoot<Guid>
{
	public decimal Amount { get; private set; }
	public string Description { get; private set; }
	public DateTime StartDate { get; private set; }
	public DateTime EndDate { get; private set; }
	public RecurrenceInterval RecurrenceInterval { get; private set; }
	public decimal BillingAmount { get; private set; }
	public Category Category { get; private set; }
	public User User { get; private set; }


	protected SubscriptionExpense() { }

	public SubscriptionExpense(decimal amount, string description, DateTime startDate, DateTime endDate, RecurrenceInterval recurrenceInterval, decimal billingAmount, Category category, User user)
	{
		Amount             = amount;
		Description        = description;
		StartDate          = startDate;
		EndDate            = endDate;
		RecurrenceInterval = recurrenceInterval;
		BillingAmount      = billingAmount;
		Category           = category;
		User               = user;
	}


	/// <summary>
	/// Validates the expense entity against the given specification and throws a SpecificationException if the specification is not satisfied.
	/// </summary>
	/// <param name="specification">The specification to validate against.</param>
	/// <exception cref="SpecificationException">Thrown if the specification is not satisfied.</exception>
	public void Validate(ISpecification<SubscriptionExpense> specification)
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
	public void Validate(ICompositeSpecification<SubscriptionExpense> specification)
	{
		var satisfactionResult = specification.IsSatisfiedBy(this);

		if (!satisfactionResult.IsSatisfied)
		{
			throw new SpecificationException(satisfactionResult.Errors);
		}
	}


	public void SetId(Guid id)
	{
		Id = id;
	}


	public void Update(decimal amount, string description, DateTime startDate, DateTime endDate, RecurrenceInterval recurrenceInterval, decimal billingAmount, Category category, User user)
	{
		Amount             = amount;
		Description        = description;
		StartDate          = startDate;
		EndDate            = endDate;
		RecurrenceInterval = recurrenceInterval;
		BillingAmount      = billingAmount;
		Category           = category;
		User               = user;
	}
}
