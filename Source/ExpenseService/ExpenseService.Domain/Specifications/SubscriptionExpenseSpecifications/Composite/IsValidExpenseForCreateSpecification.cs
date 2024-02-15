using ExpenseService.Domain.Specifications.Shared;

namespace ExpenseService.Domain.Specifications.SubscriptionExpenseSpecifications.Composite;

/// <summary>
/// Represents a specification that determines if an expense is valid for creation.
/// </summary>
public class IsValidSubscriptionExpenseForCreateSpecification : CompositeSpecification<SubscriptionExpense>
{
	public IsValidSubscriptionExpenseForCreateSpecification() : base( false )
	{
		ConfigureSpecifications();
	}

	public override void ConfigureSpecifications()
	{
		AddSpecification(new IsValidAggregateRootCreationAuditSpecification<SubscriptionExpense, Guid>());
		AddSpecification(new IsValidSubscriptionExpenseAmountSpecification());
		AddSpecification(new IsValidSubscriptionExpenseDatesSpecification());

		AddSpecification(new IsValidSubscriptionExpenseUserSpecification());
		AddSpecification(new IsValidSubscriptionExpenseCategorySpecification());
		AddSpecification(new IsValidSubscriptionExpenseDescriptionSpecification());
	}
}
