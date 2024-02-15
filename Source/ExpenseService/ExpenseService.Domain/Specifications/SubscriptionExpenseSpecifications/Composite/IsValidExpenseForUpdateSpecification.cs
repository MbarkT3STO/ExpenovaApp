using ExpenseService.Domain.Specifications.Shared;

namespace ExpenseService.Domain.Specifications.SubscriptionExpenseSpecifications.Composite;

/// <summary>
/// Represents a specification that determines if an expense is valid for update.
/// </summary>
public class IsValidSubscriptionExpenseForUpdateSpecification : CompositeSpecification<SubscriptionExpense>
{
	public IsValidSubscriptionExpenseForUpdateSpecification() : base(false)
	{
		ConfigureSpecifications();
	}

	public override void ConfigureSpecifications()
	{
		AddSpecification(new IsValidSubscriptionExpenseDatesSpecification());
		AddSpecification(new IsValidSubscriptionExpenseDescriptionSpecification());
		AddSpecification(new IsValidSubscriptionExpenseAmountSpecification());

		AddSpecification(new IsValidSubscriptionExpenseUserSpecification());
		AddSpecification(new IsValidSubscriptionExpenseCategorySpecification());

		AddSpecification(new IsValidAggregateRootUpdateAuditSpecification<SubscriptionExpense, Guid>());
	}
}
