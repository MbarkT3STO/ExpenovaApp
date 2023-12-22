using ExpenseService.Domain.Repositories;

namespace ExpenseService.Domain.Services.Category;

/// <summary>
/// Represents a service for checking category uniqueness.
/// </summary>
public interface ICategoryUniquenessChecker
{
	/// <summary>
	/// Checks if the specified category name is unique for the given user.
	/// </summary>
	/// <param name="name">The category name to check.</param>
	/// <param name="userId">The user ID associated with the category.</param>
	/// <returns>A task that represents the asynchronous operation.
	/// The task result contains true if the category name is unique; otherwise, false.</returns>
	Task<bool> IsUniqueCategoryNameAsync(string name, string userId);
}


/// <summary>
/// Represents a service for checking category uniqueness.
/// </summary>
public class CategoryUniquenessChecker : ICategoryUniquenessChecker
{
	readonly ICategoryRepository _categoryRepository;

	public CategoryUniquenessChecker(ICategoryRepository categoryRepository)
	{
		_categoryRepository = categoryRepository;
	}

	public async Task<bool> IsUniqueCategoryNameAsync(string name, string userId)
	{
		var category = await _categoryRepository.GetByNameAndUserIdAsync(name, userId);

		return category == null;
	}
}
