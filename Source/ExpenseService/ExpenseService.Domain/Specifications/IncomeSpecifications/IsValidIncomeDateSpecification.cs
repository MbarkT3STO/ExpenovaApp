namespace ExpenseService.Domain.Specifications.IncomeSpecifications;

/// <summary>
/// Represents a specification that checks if an income date is valid.
/// </summary>
public class IsValidIncomeDateSpecification : Specification<Income>
{
	protected override void ConfigureRules()
	{
		AddRule(expense => expense.Date.Date <= DateTime.UtcNow.Date, "Date must be in the past or today's date");
	}
}
