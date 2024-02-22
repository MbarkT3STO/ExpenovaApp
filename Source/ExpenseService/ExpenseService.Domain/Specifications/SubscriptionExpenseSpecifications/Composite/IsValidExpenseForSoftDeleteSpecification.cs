using ExpenseService.Domain.Specifications.Shared;

namespace ExpenseService.Domain.Specifications.SubscriptionExpenseSpecifications.Composite;

/// <summary>
/// Represents a specification that determines if an expense is valid for soft deletion.
/// </summary>
public class IsValidSubscriptionExpenseForSoftDeleteSpecification : CompositeSpecification<SubscriptionExpense>
{
	public IsValidSubscriptionExpenseForSoftDeleteSpecification() : base(false)
	{
		ConfigureSpecifications();
	}

	public override void ConfigureSpecifications()
	{
		AddSpecification(new IsValidSubscriptionExpenseDescriptionSpecification());
		AddSpecification(new IsValidSubscriptionExpenseAmountSpecification());

		AddSpecification(new IsValidSubscriptionExpenseUserSpecification());
		AddSpecification(new IsValidSubscriptionExpenseCategorySpecification());

		AddSpecification(new IsValidAggregateRootCreationAuditSpecification<SubscriptionExpense, Guid>());
		AddSpecification(new IsValidAggregateRootUpdateAuditSpecification<SubscriptionExpense, Guid>());
		AddSpecification(new IsValidAggregateRootSoftDeleteAuditSpecification<SubscriptionExpense, Guid>());
	}
}
