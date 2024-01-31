using ExpenseService.Application.Extensions;
using ExpenseService.Domain.Specifications.ExpenseSpecifications.Composite;

namespace ExpenseService.Application.ApplicationServices;

/// <summary>
/// Represents a service for managing expenses in the application layer.
/// <br/>
/// This class is an extension of the <see cref="Domain.Services.Expense.ExpenseService"/> class.
/// </summary>
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
	public async Task<Domain.Entities.Expense> GetExpenseByIdOrThrowAsync(Guid id)
	{
		var expense = await _expenseRepository.GetByIdAsync(id);

		if (expense == null)
			throw new NotFoundException($"Expense with ID {id} does not exist.");

		return expense;
	}

	/// <summary>
	/// Asynchronously updates the expense.
	/// </summary>
	/// <param name="expense">The expense to update.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task ApplyUpdateAsync(Domain.Entities.Expense expense)
	{
		// Validate the expense for update
		expense.Validate(new IsValidExpenseForUpdateSpecification());

		// Update the expense
		await _expenseRepository.UpdateAsync(expense);
	}


	/// <summary>
	/// Applies soft delete to the specified expense.
	/// </summary>
	/// <param name="expense">The expense to apply soft delete to.</param>
	/// <returns>A task representing the asynchronous operation.</returns>
	public async Task ApplySoftDeleteAsync(Domain.Entities.Expense expense)
	{
		// Validate the expense for update
		expense.Validate(new IsValidExpenseForSoftDeleteSpecification());

		// Apply soft delete
		await _expenseRepository.SoftDeleteAsync(expense);
	}

}