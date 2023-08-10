using System.Linq.Expressions;

namespace ExpenseService.Domain.Specifications;

public class ValidExpenseSpecification : Specification<Expense>
{
	public override Expression<Func<Expense, bool>> ToExpression()
	{
		return expense => expense.Amount > 0
		&& expense.Date > DateTime.MinValue 
		&& !string.IsNullOrEmpty(expense.Description);
	}
}
