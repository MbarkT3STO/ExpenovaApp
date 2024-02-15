using ExpenseService.Domain.Entities;
using ExpenseService.Domain.Specifications.Shared;


namespace ExpenseService.Domain.Specifications.SubscriptionExpenseSpecifications;

/// <summary>
/// Represents a specification that checks if an expense has a valid user.
/// </summary>
public class IsValidSubscriptionExpenseUserSpecification: Specification<SubscriptionExpense>
{
	protected override void ConfigureRules()
	{
		AddRule(entity => entity.User != null, "Invalid user for subscription expense");
		AddRule(entity => entity.User.Id != null, "User id must not be null for subscription expense");
		AddRule(entity => entity.User.Id != string.Empty, "User id must not be empty for subscription expense");
		AddRule(entity => entity.User.Id.Length <= 50, "User id must not be longer than 50 characters for subscription expense");
	}
}
