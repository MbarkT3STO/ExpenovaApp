using ExpenseService.Domain.Repositories;

namespace ExpenseService.Domain.Services;

/// <summary>
/// Represents a service for managing categories.
/// </summary>
public class CategoryService: ICategoryService
{
    readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
	{
		_categoryRepository = categoryRepository;
	}

	public async Task<bool> IsUniqueCategoryNameAsync(Guid id, string name, string userId)
	{
		var category = await _categoryRepository.GetByNameAndUserIdAsync(name, userId);

		return category == null || category.Id == id;
	}
}