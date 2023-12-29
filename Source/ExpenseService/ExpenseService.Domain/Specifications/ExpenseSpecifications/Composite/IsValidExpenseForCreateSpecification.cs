using ExpenseService.Domain.Specifications.Shared;

namespace ExpenseService.Domain.Specifications.ExpenseSpecifications.Composite;

/// <summary>
/// Represents a specification that determines if an expense is valid for creation.
/// </summary>
public class IsValidExpenseForCreateSpecification : CompositeSpecification<Expense>
{
	public IsValidExpenseForCreateSpecification()
	{
		AddSpecification(new IsValidAggregateRootCreationAuditSpecification<Expense, Guid>());
		AddSpecification(new IsValidExpenseAmountSpecification());
		AddSpecification(new IsValidExpenseDateSpecification());

		AddSpecification(new IsValidExpenseUserSpecification());
		AddSpecification(new IsValidExpenseCategorySpecification());
		AddSpecification(new IsValidExpenseDescriptionSpecification());
	}
}
