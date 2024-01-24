using ExpenseService.Domain.Specifications.Shared;

namespace ExpenseService.Domain.Specifications.ExpenseSpecifications.Composite;

/// <summary>
/// Represents a specification that determines if an expense is valid for update.
/// </summary>
public class IsValidExpenseForUpdateSpecification : CompositeSpecification<Expense>
{
	public IsValidExpenseForUpdateSpecification() : base(false)
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

		AddSpecification(new IsValidAggregateRootUpdateAuditSpecification<Expense, Guid>());

	}
}
