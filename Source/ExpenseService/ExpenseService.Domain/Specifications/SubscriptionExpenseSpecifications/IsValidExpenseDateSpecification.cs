namespace ExpenseService.Domain.Specifications.SubscriptionExpenseSpecifications;

/// <summary>
/// Represents a specification that checks if the expense dates are valid for a subscription expense.
/// </summary>
public class IsValidSubscriptionExpenseDatesSpecification : Specification<SubscriptionExpense>
{
	protected override void ConfigureRules()
	{
		AddRule(expense => expense.StartDate.Date <= expense.EndDate.Date, "Start date must be before or equal to end date for subscription expense");
		AddRule(expense => expense.StartDate.Date >= DateTime.Now.Date, "Start date must be in the future for subscription expense");

		AddRule(expense => expense.EndDate.Date >= DateTime.Now.Date, "End date must be in the future for subscription expense");
		AddRule(expense => expense.EndDate.Date >= expense.StartDate.Date, "End date must be after or equal to start date for subscription expense");
	}
}
