using ExpenseService.Application.Extensions;
using ExpenseService.Domain.Specifications.ExpenseSpecifications.Composite;

namespace ExpenseService.Application.ApplicationServices;

/// <summary>
/// Represents a service for managing expenses in the application layer.
/// <br/>
/// This class is an extension of the <see cref="Domain.Services.Expense.ExpenseService"/> class.
/// </summary>
public class ApplicationExpenseService: Domain.Services.Expense.ExpenseService
{
	readonly ApplicationCategoryService _categoryService;

	public IExpenseRepository Repository { get; }


	public ApplicationExpenseService(IExpenseRepository expenseRepository, ApplicationCategoryService categoryService): base(expenseRepository)
	{
		_categoryService = categoryService;
		Repository       = expenseRepository;
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


	/// <summary>
	/// Retrieves a list of expenses for a given user ID.
	/// </summary>
	/// <param name="userId">The ID of the user.</param>
	/// <returns>A collection of expenses.</returns>
	public async Task<IEnumerable<Domain.Entities.Expense>> GetExpensesByUserIdAsync(string userId)
	{
		var expenses = await _expenseRepository.GetExpensesByUserAsync(userId);

		return expenses;
	}


	public async Task<IEnumerable<Domain.Entities.Expense>> GetExpensesByCategory(Guid categoryId)
	{
		var category = await _categoryService.GetCategoryOrThrowAsync(categoryId);
		var expenses = await _expenseRepository.GetExpensesByCategoryAsync(categoryId);

		return expenses;
	}

}