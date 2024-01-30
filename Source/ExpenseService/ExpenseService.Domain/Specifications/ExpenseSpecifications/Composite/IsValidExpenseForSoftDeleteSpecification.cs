using ExpenseService.Domain.Specifications.Shared;

namespace ExpenseService.Domain.Specifications.ExpenseSpecifications.Composite;

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

		AddSpecification(new IsValidAggregateRootUpdateAuditSpecification<Expense, Guid>());
	}
}
