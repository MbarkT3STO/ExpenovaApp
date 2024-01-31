using ExpenseService.Domain.Specifications.Shared;

namespace ExpenseService.Domain.Specifications.ExpenseSpecifications.Composite;

/// <summary>
/// Represents a specification that determines if an expense is valid for soft deletion.
/// </summary>
public class IsValidExpenseForSoftDeleteSpecification : CompositeSpecification<Expense>
{
	public IsValidExpenseForSoftDeleteSpecification() : base(false)
	{
		ConfigureSpecifications();
	}

	public override void ConfigureSpecifications()
	{
		AddSpecification(new IsValidExpenseDateSpecification());
		AddSpecification(new IsValidExpenseDescriptionSpecification());
		AddSpecification(new IsValidExpenseAmountSpecification());

		AddSpecification(new IsValidExpenseUserSpecification());
		AddSpecification(new IsValidExpenseCategorySpecification());

		AddSpecification(new IsValidAggregateRootCreationAuditSpecification<Expense, Guid>());
		AddSpecification(new IsValidAggregateRootUpdateAuditSpecification<Expense, Guid>());
		AddSpecification(new IsValidAggregateRootSoftDeleteAuditSpecification<Expense, Guid>());
	}
}
