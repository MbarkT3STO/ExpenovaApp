using System.Linq.Expressions;

namespace ExpenseService.Domain.Specifications.ExpenseSpecifications;

public class ValidExpenseSpecification : Specification<Expense>
{
    protected override string UnSatisfiedSpecificationErrorMessage => "Expense is invalid.";

    public override Expression<Func<Expense, bool>> ToExpression()
	{
		return expense => expense.Amount > 0
		&& expense.Date > DateTime.MinValue 
		&& !string.IsNullOrEmpty(expense.Description);
	}
}
