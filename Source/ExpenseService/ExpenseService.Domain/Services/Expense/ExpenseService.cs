
using ExpenseService.Domain.Repositories;

namespace ExpenseService.Domain.Services.Expense;

public class ExpenseService : IExpenseService
{

	protected readonly IExpenseRepository _expenseRepository;

	public ExpenseService(IExpenseRepository expenseRepository)
	{
		_expenseRepository = expenseRepository;
	}

}
