using ExpenseService.Application.Extensions;

namespace ExpenseService.Application.ApplicationServices;

public class ApplicationExpenseService : Domain.Services.Expense.ExpenseService
{
	public ApplicationExpenseService(IExpenseRepository expenseRepository) : base(expenseRepository)
	{

	}

	/// <summary>
	/// Retrieves an expense by its ID or throws a <see cref="NotFoundException"/> if it does not exist.
	/// </summary>
	/// <param name="id">The ID of the expense to retrieve.</param>
	/// <returns>The expense with the specified ID.</returns>
	public async Task<Domain.Entities.Expense> GetExpenseIdOrThrowAsync(Guid id)
	{
		var expense = await _expenseRepository.GetByIdAsync(id);

		if (expense == null)
			throw new NotFoundException($"Expense with ID {id} does not exist.");

		return expense;
	}
}