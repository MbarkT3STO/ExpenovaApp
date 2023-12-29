namespace ExpenseService.Application.ApplicationServices;

public class ApplicationExpenseService : Domain.Services.Expense.ExpenseService
{
    public ApplicationExpenseService(IExpenseRepository expenseRepository) : base(expenseRepository)
    {

    }
}