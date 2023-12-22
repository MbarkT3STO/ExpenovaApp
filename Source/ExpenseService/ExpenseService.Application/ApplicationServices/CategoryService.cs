namespace ExpenseService.Application.ApplicationServices;

public class CategoryService
{
	private readonly ICategoryRepository _categoryRepository;


	public CategoryService(ICategoryRepository categoryRepository)
	{
		_categoryRepository = categoryRepository;
	}

	/// <summary>
	/// Checks if the category name is unique for the specified user.
	/// </summary>
	/// <param name="name">The name of the category.</param>
	/// <param name="userId">The ID of the user.</param>
	/// <returns>True if the category name is unique, false otherwise.</returns>
	public async Task<bool> IsCategoryNameUniqueAsync(string name, string userId)
	{
		var category = await _categoryRepository.GetByNameAndUserIdAsync(name, userId);

		return category == null;
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
