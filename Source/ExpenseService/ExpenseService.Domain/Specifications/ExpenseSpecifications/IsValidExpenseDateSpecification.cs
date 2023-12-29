namespace ExpenseService.Domain.Specifications.ExpenseSpecifications;

/// <summary>
/// Represents a specification that checks if the expense date is valid.
/// </summary>
public class IsValidExpenseDateSpecification : Specification<Expense>
{
	protected override void ConfigureRules()
	{
		AddRule(expense => expense.Date.Date <= DateTime.UtcNow.Date, "Date must be in the past");
	}
}
