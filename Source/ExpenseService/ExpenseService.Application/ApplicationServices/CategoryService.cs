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
}
