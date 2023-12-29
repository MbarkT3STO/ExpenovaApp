using ExpenseService.Application.Extensions;
using ExpenseService.Domain.Entities;
using ExpenseService.Domain.Services;

namespace ExpenseService.Application.ApplicationServices;

/// <summary>
/// Represents an application service for managing categories, which extends the Category domain service.
/// </summary>
public class ApplicationCategoryService : CategoryService
{
	private readonly ICategoryRepository _categoryRepository;

	public ApplicationCategoryService(ICategoryRepository categoryRepository) : base(categoryRepository)
	{
		_categoryRepository = categoryRepository;
	}

	/// <summary>
	/// Checks if a category with the specified ID exists.
	/// </summary>
	/// <param name="categoryId">The ID of the category to check.</param>
	/// <returns>True if the category exists; otherwise, false.</returns>
	public async Task<bool> IsExistAsync(Guid categoryId)
	{
		var category = await _categoryRepository.GetByIdAsync(categoryId);

		return category != null;
	}


	/// <summary>
	/// Retrieves a category by its ID and user ID, or throws an exception if it does not exist.
	/// </summary>
	/// <param name="id">The ID of the category.</param>
	/// <param name="userId">The ID of the user.</param>
	/// <returns>The category with the specified ID and user ID.</returns>
	/// <exception cref="NotFoundException">Thrown if the category does not exist.</exception>
	public async Task<Category> GetCategoryOrThrowExceptionIfNotExistsAsync(Guid id, string userId)
	{
		var category = await _categoryRepository.GetByIdAndUserIdAsync(id, userId);

		if (category is null)
			throw new NotFoundException($"Category with the ID {id} does not exist");

		return category;
	}
}
