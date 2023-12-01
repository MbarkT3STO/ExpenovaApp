using System.Linq.Expressions;

namespace ExpenseService.Domain.Specifications.ExpenseSpecifications;

public class ValidExpenseSpecification : Specification<Expense>
{
	protected override void ConfigureConditions()
	{
		AddCondition( expense => expense.Description != null, "Invalid description." );
		AddCondition( expense => expense.Amount > 0, "Amount must be greater than 0." );
	}
}
