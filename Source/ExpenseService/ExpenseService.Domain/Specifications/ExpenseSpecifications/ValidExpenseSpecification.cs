using System.Linq.Expressions;

namespace ExpenseService.Domain.Specifications.ExpenseSpecifications;

public class ValidExpenseSpecification : Specification<Expense>
{
	protected override void ConfigureRules()
	{
		AddRule( expense => expense.Description != null, "Invalid description." );
		AddRule( expense => expense.Amount > 0, "Amount must be greater than 0." );
	}
}
